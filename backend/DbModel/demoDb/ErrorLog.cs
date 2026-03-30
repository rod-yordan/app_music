using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbModel.demoDb;

[Table("error_log")]
public partial class ErrorLog
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("message")]
    [MaxLength(2000)]
    public string Message { get; set; } = null!;

    [Column("stack_trace")]
    public string? StackTrace { get; set; }

    [Column("path")]
    [MaxLength(500)]
    public string? Path { get; set; }

    [Column("method")]
    [MaxLength(10)]
    public string? Method { get; set; }

    [Column("user_id")]
    [MaxLength(255)]
    public string? UserId { get; set; }

    [Column("status_code")]
    [MaxLength(10)]
    public string? StatusCode { get; set; }

    [Column("timestamp")]
    public DateTime Timestamp { get; set; }
}