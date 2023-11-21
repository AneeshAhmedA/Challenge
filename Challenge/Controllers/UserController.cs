using AutoMapper;
using Challenge.DTO;
using Challenge.Entity;
using Challenge.Models;
using Challenge.Service;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Text;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private static readonly ILog log = LogManager.GetLogger(typeof(UserController));

    public UserController(IUserService userService, IMapper mapper, IConfiguration configuration)
    {
        _userService = userService;
        _mapper = mapper;
        _configuration = configuration;
    }

    [HttpPost]
    [Route("AddUser")]
    [AllowAnonymous]
    public ActionResult AddUser(UserDTO userDTO)
    {
        try
        {
            User user = _mapper.Map<User>(userDTO);
            _userService.AddUser(user);
            log.Info($"User {user.UserName} added successfully.");
            return StatusCode(200);
        }
        catch (Exception ex)
        {
            log.Error($"Error adding user: {ex.Message}", ex);
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut]
    [Route("UpdateUser")]
    [Authorize(Roles = "User")]
    public ActionResult UpdateUser([FromBody] UserDTO userDTO)
    {
        try
        {
            User user = _mapper.Map<User>(userDTO);
            _userService.UpdateUser(user);
            log.Info($"User {user.UserName} updated successfully.");
            return StatusCode(200, user);
        }
        catch (Exception ex)
        {
            log.Error($"Error updating user: {ex.Message}", ex);
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    [Route("ValidateUser")]
    [AllowAnonymous]
    public IActionResult ValidateUser(Login login)
    {
        try
        {
            User validatedUser = _userService.ValidateUser(login.Email, login.Password);
            AuthResponse authResponse = new AuthResponse();

            if (validatedUser != null)
            {
                authResponse.UserName = validatedUser.UserName;
                authResponse.Role = validatedUser.Role;
                authResponse.Token = GetToken(validatedUser);

                log.Info($"User {validatedUser.UserName} validated successfully.");
                return StatusCode(200, authResponse);
            }
            else
            {
                log.Warn($"Invalid credentials for email: {login.Email}");
                return StatusCode(401, "Invalid credentials");
            }
        }
        catch (Exception ex)
        {
            log.Error($"Error validating user: {ex.Message}", ex);
            return StatusCode(500, ex.Message);
        }
    }

    private string GetToken(User? user)
    {
        try
        {
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature
            );

            var subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Email, user.Email),
            });

            var expires = DateTime.UtcNow.AddMinutes(10);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                Expires = expires,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            return jwtToken;
        }
        catch (Exception ex)
        {
            log.Error($"Error generating JWT token: {ex.Message}", ex);
            throw;
        }
    }
}
