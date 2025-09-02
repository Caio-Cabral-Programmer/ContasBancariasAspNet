using ContasBancariasAspNet.Api.DTOs;
using ContasBancariasAspNet.Api.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace ContasBancariasAspNet.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IValidator<UserDto> _validator;

    public UserController(IUserService userService, IValidator<UserDto> validator)
    {
        _userService = userService;
        _validator = validator;
    }

    /// <summary>
    /// Get all users
    /// </summary>
    /// <returns>Retrieve a list of all registered users</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<UserDto>>> FindAll()
    {
        var users = await _userService.FindAllAsync();
        var userDtos = users.Select(u => new UserDto(u));
        return Ok(userDtos);
    }

    /// <summary>
    /// Get a user by ID
    /// </summary>
    /// <param name="id">User ID</param>
    /// <returns>Retrieve a specific user based on its ID</returns>
    [HttpGet("{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDto>> FindById(long id)
    {
        var user = await _userService.FindByIdAsync(id);
        return Ok(new UserDto(user));
    }

    /// <summary>
    /// Create a new user
    /// </summary>
    /// <param name="userDto">User data</param>
    /// <returns>Create a new user and return the created user's data. OBS.: Para realizar o create (POST), é necessário apagar do JSON todos os IDs, pois o DB irá gerar automaticamente.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<UserDto>> Create([FromBody] UserDto userDto)
    {
        var validationResult = await _validator.ValidateAsync(userDto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var user = await _userService.CreateAsync(userDto.ToModel());
        var createdUserDto = new UserDto(user);

        return CreatedAtAction(
            nameof(FindById),
            new { id = user.Id },
            createdUserDto);
    }

    /// <summary>
    /// Update a user
    /// </summary>
    /// <param name="id">User ID</param>
    /// <param name="userDto">User data</param>
    /// <returns>Update the data of an existing user based on its ID. OBS.: Para fazer o update, é necessário enviar, no JSON que vai para a API, os IDs do usuário (user), conta (account) e cartão (card).</returns>
    [HttpPut("{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<UserDto>> Update(long id, [FromBody] UserDto userDto)
    {
        var validationResult = await _validator.ValidateAsync(userDto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var user = await _userService.UpdateAsync(id, userDto.ToModel());
        return Ok(new UserDto(user));
    }

    /// <summary>
    /// Delete a user
    /// </summary>
    /// <param name="id">User ID</param>
    /// <returns>Delete an existing user based on its ID</returns>
    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(long id)
    {
        await _userService.DeleteAsync(id);
        return NoContent();
    }
}