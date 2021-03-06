﻿using Discord.WebSocket;
using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace DiscordBot
{
	public class CommandHandler
	{
        private DiscordSocketClient _client;
        private CommandService _service;

		public CommandHandler(DiscordSocketClient client)
		{
            _client = client;

            _service = new CommandService();


            _service.AddModulesAsync(Assembly.GetEntryAssembly());

            _client.MessageReceived += HandleCommandAsync;
        }

        private async Task HandleCommandAsync(SocketMessage s)
        {
            var msg = s as SocketUserMessage;
            if (msg == null) return;

            var context = new SocketCommandContext(_client, msg);

            int argPos = 123;
            if (msg.HasStringPrefix("gil ", ref argPos) || msg.HasStringPrefix("Gil ", ref argPos))
            {
                var result = await _service.ExecuteAsync(context, argPos);

                if (!result.IsSuccess && result.Error != CommandError.UnknownCommand)
                {
                    await context.Channel.SendMessageAsync(result.ErrorReason);
                }
            }
        }
    }
}
