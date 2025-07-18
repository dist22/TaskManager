namespace TaskManager.Infrastructure.Data.Configuration;

public class JwtOptions
{
    public string TokenSecret { get; set; } = string.Empty;
    public int ExpireHours { get; set; }
}