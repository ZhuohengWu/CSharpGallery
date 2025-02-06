using AutoMapper;
using Clean.Application.Commons.Data;
using Clean.Application.Commons.Responses;
using Clean.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Application.Features.ToDo.Update
{
    public class UpdateToDoTaskHandler(IMapper mapper, ICleanDbContext context) : IRequestHandler<UpdateToDoTaskCommand, ServiceResponse<Guid>>
    {
        public async Task<ServiceResponse<Guid>> Handle(UpdateToDoTaskCommand request, CancellationToken cancellationToken)
        {
            var mapDto = mapper.Map<ToDoTask>(request.UpdateToDo);
            if (mapDto == null)
                return ServiceResponse<Guid>.Failure("Invalid request");

            context.TodoItems.Update(mapDto);
            await context.SaveChangesAsync(cancellationToken);
            return ServiceResponse<Guid>.Success(Guid.Empty, "Task updated");
        }
    }
}
