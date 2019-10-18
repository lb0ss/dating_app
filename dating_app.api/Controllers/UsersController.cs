using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using dating_app.api.Data;
using dating_app.api.Dtos;
using dating_app.api.Helpers;
using dating_app.api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dating_app.api.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IDatingRepository _repo;
        private readonly IMapper _mapper;

        public object UserForDetaildDto { get; private set; }

        public UsersController(IDatingRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery]UserParams userParams)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var userFromRepo = await _repo.GetUser(currentUserId);

            userParams.UserId = currentUserId;

            if (string.IsNullOrEmpty(userParams.Gender))        // if no gender is specified in the user params
            {
                userParams.Gender = userFromRepo.Gender == "male" ? "female" : "male";
            }

            var users = await _repo.GetUsers(userParams);

            var usersToReturn = _mapper.Map<IEnumerable<UserForListDto>>(users);

            Response.AddPagination(users.CurrentPage, users.PageSize,
            users.TotalCount, users.TotalPages);

            return Ok(usersToReturn);
        }

       [HttpGet("{id}", Name="GetUser")]
       public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.GetUser(id);

            var userToReturn = _mapper.Map<UserForDetailedDto>(user);

            return Ok(userToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserForUpdateDto userForUpdateDto)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var userFromRepo = await _repo.GetUser(id);

            _mapper.Map(userForUpdateDto, userFromRepo); // update the values from userForUpdateDto and write them into userFromRepo

            if (await _repo.SaveAll())
                return NoContent();     // returns NoContent if the save operation is successful
            
            throw new Exception($"Updating user {id} failed on save");

        }

        [HttpPost("{id}/like/{recipientId}")]
        public async Task<IActionResult> LikeUser(int id, int recipientId)
        {
                if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

                var like = await _repo.GetLike(id, recipientId);

                if (like != null) {     // check if the recipient has already been liked
                    return BadRequest("You already liked this user");
                }

                if (await _repo.GetUser(recipientId) == null)   // check if the like recipient exists
                {
                    return NotFound();
                }

                like = new Like
                {
                    LikerId = id,
                    LikeeId = recipientId
                };

                _repo.Add<Like>(like);      // add to repo memory

                if (await _repo.SaveAll())  // save to repo
                    return Ok();

                return BadRequest("Failed to like user");

        }
    }
}