using AutoMapper.QueryableExtensions;
using AutoMapper;
using Clean.Application.Commons.Data;
using Clean.Application.Commons.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Application.Features.ToDo.GetAll
{
    public class GetAllToDoTaskHandler(ICleanDbContext context, IMapper mapper) : IRequestHandler<GetAllToDoTaskQuery, ServiceResponse<IEnumerable<GetToDoTask>>>
    {
        public async Task<ServiceResponse<IEnumerable<GetToDoTask>>> Handle(GetAllToDoTaskQuery request, CancellationToken cancellationToken)
        {
            //var getTasks = await context.TodoItems
            //    .AsNoTracking()
            //    .Select(x => new GetToDoTask
            //    {
            //        Description = x.Description,
            //        Title = x.Title,
            //        DueDate = x.DueDate,
            //        IsCompleted = x.IsCompleted,
            //        Priority = x.Priority.ToString(),
            //    })
            //    .ToListAsync(cancellationToken);

            var getTasks = await context.TodoItems
                .AsNoTracking()
                .ProjectTo<GetToDoTask>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return getTasks.Count > 0
                ? ServiceResponse<IEnumerable<GetToDoTask>>.Success(getTasks, "Tasks found")
                : new ServiceResponse<IEnumerable<GetToDoTask>>([], true, "Null");
        }
    }
}
