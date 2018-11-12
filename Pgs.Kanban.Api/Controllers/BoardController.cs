﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pgs.Kanban.Domain.Dtos;
using Pgs.Kanban.Domain.Services;
using Pgs.Kanban.Domain.Services.Interfaces;

namespace Pgs.Kanban.Api.Controllers
{
    [Route("api/[controller]")]
    public class BoardController : Controller
    {
        private readonly IBoardService _boardService;

        public BoardController(IBoardService boardService)
        {
            _boardService = boardService;
        }

        [HttpGet]
        public IActionResult GetBoard()
        {
            var response = _boardService.GetBoard();

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPost]
        public IActionResult CreateBoard([FromBody] CreateBoardDto createBoardDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = _boardService.CreateBoard(createBoardDto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult EditBoard([FromBody] EditBoardNameDto editBoardNameDto, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = _boardService.EditBoard(editBoardNameDto, id);

            if (!result)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
