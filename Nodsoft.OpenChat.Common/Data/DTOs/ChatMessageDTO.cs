using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodsoft.OpenChat.Common.Data.DTOs;

public record ChatMessageDTO<TUserId> where TUserId : unmanaged
{
	/// <summary>
	/// ID of message
	/// </summary>
	[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public Guid Id { get; init; }

	/// <summary>
	/// ID of message's author
	/// </summary>
	public TUserId UserId { get; init; }

	/// <summary>
	/// Message content
	/// </summary>
	public string Content { get; set; } = string.Empty;
}
