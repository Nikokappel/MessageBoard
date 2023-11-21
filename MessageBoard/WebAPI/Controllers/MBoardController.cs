using Application.LogicInterfaces;
using Domain.Models;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class MBoardController : ControllerBase
{
    private readonly IMBoardLogic mBoardLogic;

    public MBoardController(IMBoardLogic mBoardLogic)
    {
        this.mBoardLogic = mBoardLogic;
    }
    
    [HttpPost]
    public async Task<ActionResult<MessageBoard>> CreateAsync([FromBody]MBoardCreationDto dto)
    {
        try
        {
            MessageBoard created = await mBoardLogic.CreateAsync(dto);
            return Created($"/messageBoards/{created.Id}", created);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MessageBoard>>> GetAsync([FromQuery] string? username,[FromQuery] int? userId,[FromQuery] string? titleContains, [FromQuery] string? bodyContains)
    {
        try
        {
            SearchMBoardParametersDto parameters = new(username, userId, titleContains, bodyContains);
            IEnumerable<MessageBoard> messageBoards = await mBoardLogic.GetAsync(parameters);
            return Ok(messageBoards);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPatch]
    public async Task<ActionResult<MessageBoard>> UpdateAsync([FromBody]MBoardUpdateDto toUpdate)
    {
        try
        {
            await mBoardLogic.UpdateAsync(toUpdate);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync([FromRoute] int id)
    {
        try
        {
            await mBoardLogic.DeleteAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetById([FromRoute] int id)
    {
        try
        {
            MBoardBasicDto result = await mBoardLogic.GetByIdAsync(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}