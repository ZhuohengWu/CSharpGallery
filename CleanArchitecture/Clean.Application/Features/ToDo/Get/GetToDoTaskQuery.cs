﻿using Clean.Application.Commons.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Application.Features.ToDo.Get;

public record GetToDoTaskQuery(Guid Id) : IRequest<ServiceResponse<GetToDoTask>>;
