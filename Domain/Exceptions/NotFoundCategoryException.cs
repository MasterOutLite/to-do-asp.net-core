using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Exceptions.Base;

namespace Domain.Exceptions;

public sealed class NotFoundCategoryException(long id) :
    NotFoundException($"Not found Category by id {id}!");