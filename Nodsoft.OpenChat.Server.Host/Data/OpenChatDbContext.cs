#nullable disable

using Microsoft.EntityFrameworkCore;
using Nodsoft.OpenChat.Server.Data.Models;

namespace Nodsoft.OpenChat.Server.Data;

public class OpenChatDbContext : DbContext, IOpenChatDbContext
{
	public DbSet<ChatUser<ulong>> Users { get; set; }
	public DbSet<ChatMessage<ulong>> Messages { get; set; }


	public OpenChatDbContext(DbContextOptions<OpenChatDbContext> dbContextOptions) : base(dbContextOptions) { }


	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
	}
}
