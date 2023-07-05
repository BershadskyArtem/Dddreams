using Dddreams.Domain.Common;
using Dddreams.Domain.Enums;

namespace Dddreams.Domain.Entities;

public class Like : Likable
{
    public DreamsAccount Author { get; set; }
}