using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Infrastructure
{
    public interface IUserDictionary
    {
        string Id { get; set; }

        string Name { get; set; }

        string SystemId { get; set; }

        bool Enable { get; set; }

        [ScaffoldColumn(false)]
        [NotMapped]
        bool Selected { get; set; }
    }

}
