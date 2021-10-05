using AutoMapper;
using Hahn.ApplicatonProcess.July2021.Domain.DTOs;
using Hahn.ApplicatonProcess.July2021.Domain.Interfaces;
using Hahn.ApplicatonProcess.July2021.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.July2021.Web.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        //[HttpGet]
        //public async Task<ActionResult<List<User>>> Get()
        //{
        //    var users = await _userService.GetUsers();
        //    //var usersDTO = _mapper.Map<UserDTO>(users);
        //    return Ok(users);
        //}

        /// <summary>
        /// This method return a user by his id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<UserDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<UserDTO>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Get(int id)
        {
            var userDTO = new UserDTO();
            var user = await _userService.GetUser(id);
            userDTO = _mapper.Map<UserDTO>(user);
            return Ok(userDTO);
        }

        /// <summary>
        /// This enpoint receives a json object and creates a user with that information
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status201Created)]
        public async Task<ActionResult> Post([FromBody] UserDTO userDTO)
        {
            var userCreatedDTO = new UserDTO();
            var user = _mapper.Map<User>(userDTO);
            var userCreated = await _userService.AddUser(user);
            userCreatedDTO = _mapper.Map<UserDTO>(userCreated);
            return Created(nameof(Get), new { id = userCreatedDTO.Id, userCreatedDTO });
        }

        /// <summary>
        /// this enpoint receives a user and updates it
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put(int id, [FromBody] UserDTO userDTO)
        {
            var user = _mapper.Map<User>(userDTO);
            user.Id = id;
            await _userService.UpdateUser(user);
            return NoContent();
        }

        /// <summary>
        /// this endpoint receives an id of a user and removes it
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            await _userService.DeleteUser(id);
            return NoContent();
        }
    }
}
