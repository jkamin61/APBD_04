using APBD_04.DTO;
using APBD_04.Service;
using Microsoft.AspNetCore.Mvc;

namespace APBD_04.Controllers;

[ApiController]
[Route("api/pcs")]
public class PcsController : ControllerBase
{
    private readonly IPcService _pcService;

    public PcsController(IPcService pcService)
    {
        _pcService = pcService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PcDto>>> GetAll()
    {
        return Ok(await _pcService.GetAllAsync());
    }

    [HttpGet("{id:int}/components")]
    public async Task<ActionResult<PcWithComponentsDto>> GetComponents(int id)
    {
        var pc = await _pcService.GetWithComponentsAsync(id);
        if (pc is null)
        {
            return NotFound();
        }

        return Ok(pc);
    }

    [HttpPost]
    public async Task<ActionResult<PcDto>> Create([FromBody] PcCreateUpdateDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var created = await _pcService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetComponents), new { id = created!.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<PcDto>> Update(int id, [FromBody] PcCreateUpdateDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var updated = await _pcService.UpdateAsync(id, dto);
        if (updated is null)
        {
            return NotFound();
        }

        return Ok(updated);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (!await _pcService.DeleteAsync(id))
        {
            return NotFound();
        }

        return NoContent();
    }
}
