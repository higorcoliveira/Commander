using System.Collections.Generic;
using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers
{
    [Route("api/commands")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        // Configured on Startup
        private readonly ICommanderRepo _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommanderRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult <IEnumerable<CommanderReadDTO>> GetAllCommands()
        {
            var commandItems = _repository.GetAllCommands();
            return Ok(_mapper.Map<IEnumerable<CommanderReadDTO>>(commandItems));
        }

        [HttpGet("{id}", Name="GetCommandById")]
        public ActionResult <CommanderReadDTO> GetCommandById(int id)
        {
            var commandItem = _repository.GetCommandById(id);
            if (commandItem != null) 
            {
                return Ok(_mapper.Map<CommanderReadDTO>(commandItem));
            }
            return NotFound();            
        }

        //POST api/commands
        [HttpPost]
        public ActionResult <CommanderReadDTO> CreateCommand(CommanderCreateDTO commanderCreateDTO)
        {
            var commandModel = _mapper.Map<Command>(commanderCreateDTO);
            _repository.CreateCommand(commandModel);
            _repository.SaveChanges(); // commit

            var commanderReadDTO = _mapper.Map<CommanderReadDTO>(commandModel);

            return CreatedAtRoute(nameof(GetCommandById), new {Id = commanderReadDTO.Id}, commanderReadDTO);
        }

        // PUT api/commands/{id}
        [HttpPut("{id}")]
        public ActionResult <CommanderReadDTO> UpdateCommand(int id, CommanderUpdateDTO commanderUpdateDTO)
        {
            var commandFromRepo = _repository.GetCommandById(id);
            if (commandFromRepo == null) 
            {
                return NotFound();
            }
            // mapeia os dados vindos do DTO no modelo existente
            _mapper.Map(commanderUpdateDTO, commandFromRepo);
            _repository.SaveChanges(); // commit
            return NoContent();
        }

        // PATCH api/commands/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommanderUpdateDTO> patchDocument)
        {
            var commandFromRepo = _repository.GetCommandById(id);
            if (commandFromRepo == null) 
            {
                return NotFound();
            }
            var commandToPatch = _mapper.Map<CommanderUpdateDTO>(commandFromRepo);
            patchDocument.ApplyTo(commandToPatch, ModelState); // ModelState faz validações no modelo
            if (!TryValidateModel(commandToPatch)) 
            {
                return ValidationProblem(ModelState);
            }
            // mapeia os dados vindos do DTO no modelo existente
            _mapper.Map(commandToPatch, commandFromRepo);
            _repository.SaveChanges(); // commit
            return NoContent();
        }

        // DELETE api/commands/{id}
        [HttpDelete("{id}")]
        public ActionResult CommandDelete(int id) 
        {
            var commandFromRepo = _repository.GetCommandById(id);
            if (commandFromRepo == null) 
            {
                return NotFound();
            }
            _repository.DeleteCommand(commandFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }
    }
}