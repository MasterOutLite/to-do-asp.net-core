﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models;

public record CategoryResponse(long Id, string Name, string Description, long UserId);