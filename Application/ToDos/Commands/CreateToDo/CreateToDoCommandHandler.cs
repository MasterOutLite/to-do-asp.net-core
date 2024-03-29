﻿using Application.Abstractions.Interfaces;
using Application.Abstractions.Messaging;
using Application.Common.Models;
using Domain.Abstractions.Repository;
using Domain.Entities;
using Domain.Exceptions;
using Mapster;
using Serilog;

namespace Application.ToDos.Commands.CreateToDo;

public class CreateToDoCommandHandler(
    IToDoRepository toDoRepository,
    IUnitOfWork unitOfWork
)
    : ICommandHandler<CreateToDoCommand, ToDoResponse>
{
    public async Task<ToDoResponse> Handle(CreateToDoCommand request, CancellationToken cancellationToken)
    {
        var entity = request.Adapt<ToDo>();
        Log.Information("Entity {@Entity}. Dto {@Dto}", entity, request);

        toDoRepository.Add(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Adapt<ToDoResponse>();
    }
}