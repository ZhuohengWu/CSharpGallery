//using AutoMapper;
//using eCommerceClean.Application.Commons.Data;
//using eCommerceClean.Application.Commons.Responses;
//using eCommerceClean.Domain.Entities;
//using MediatR;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace eCommerceClean.Application.Features.ToDo.Update
//{
//    public class UpdateToDoTaskHandler(IMapper mapper, IStoreDbContext context) : IRequestHandler<UpdateToDoTaskCommand, ServiceResponse<Guid>>
//    {
//        public async Task<ServiceResponse<Guid>> Handle(UpdateToDoTaskCommand request, CancellationToken cancellationToken)
//        {
//            var mapDto = mapper.Map<ToDoTask>(request.UpdateToDo);
//            if (mapDto == null)
//                return ServiceResponse<Guid>.Failure("Invalid request");

//            context.TodoItems.Update(mapDto);
//            await context.SaveChangesAsync(cancellationToken);
//            return ServiceResponse<Guid>.Success(Guid.Empty, "Task updated");
//        }
//    }
//}
