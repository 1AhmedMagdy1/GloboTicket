using GloboTicket.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Application.Features.EventCategory.DeleteCategory
{
    public class DeleteCategoryCommand : IRequest<Result<bool>>
    {
        public int ID { get; set; }
    }
}
