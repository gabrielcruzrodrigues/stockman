﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using stockman.Models;
using stockman.Models.Dtos;
using stockman.Repositories.Interfaces;
using stockman.ViewModels;

namespace stockman.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CallController : ControllerBase
    {
        private readonly ICallRepository _callRepository;
        private readonly IUsersRepository _userRepository;
        private readonly ISectorRepository _sectorRepository;
        public CallController(
            ICallRepository callRepository, 
            IUsersRepository userRepository, 
            ISectorRepository sectorRepository
        )
        {
            _callRepository = callRepository;
            _userRepository = userRepository;
            _sectorRepository = sectorRepository;
        }

        [HttpPost]
        [Authorize(policy: "user")]
        public async Task<ActionResult<Call>> Create(CreateCallViewModel request)
        {
            _ = await _userRepository.GetByIdAsync(request.UserId);
            _ = await _sectorRepository.GetByIdAsync(request.SectorId);

            Call call = request.CreateCall();
            var result = await _callRepository.CreateAsync(call);
            return StatusCode(201, result);
        }

        [HttpGet]
        [Authorize(policy: "moderador")]
        public async Task<ActionResult<IEnumerable<CallDto>>> GetAll()
        {
            return Ok(await _callRepository.GetAllAsync());
        }

        [HttpGet("{callId:long}")]
        [Authorize(policy: "moderador")]
        public async Task<ActionResult<Call>> GetById(long callId)
        {
            if (callId <= 0)
            {
                return BadRequest("O id para pesquisa deve ser maior que zero");
            }

            return Ok(await _callRepository.GetByIdAsync(callId));
        }

        //[HttpDelete("{calledId:int}")]
        //public async Task<IActionResult> Delete(int calledId)
        //{
        //    if (calledId <= 0)
        //    {
        //        return BadRequest("The id for search called must be greater than zero");
        //    }

        //    Called called = await _callRepository.GetByIdAsync(calledId);
        //    await _callRepository.DeleteAsync(called);
        //    return NoContent();
        //}

        [HttpGet("user/{userId:long}")]
        [Authorize(policy: "moderador")]
        public async Task<ActionResult<IEnumerable<Call>>> GetByUserId(long userId)
        {
            if (userId <= 0)
            {
                return BadRequest("O id para pesquisa deve ser maior que zero");
            }

            return Ok(await _callRepository.GetByUserIdAsync(userId));
        }

        [HttpGet("sector/{sectorId:int}")]
        [Authorize(policy: "moderador")]
        public async Task<ActionResult<IEnumerable<Call>>> GetBysectorId(int sectorId)
        {
            if (sectorId <= 0)
            {
                return BadRequest("O id para pesquisa deve ser maior que zero");
            }

            return Ok(await _callRepository.GetBySectorIdAsync(sectorId));
        }
    }
}

