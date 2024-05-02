using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ConcurrencySqlServer.Models.Database;

public partial class Book
{
    [Key]
    public int ID { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column(TypeName = "money")]
    public decimal Price { get; set; }

    public byte[] RowVersion { get; set; } = null!;
}
