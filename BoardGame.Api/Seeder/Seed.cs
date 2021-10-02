using API.Entities.Context;
using BoardGame.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace BoardGame.Api.Seeder
{
    public class Seed
    {
        public static async Task SeedData(DataContext context)
        {
            if (await context.BlockTypes.AnyAsync()) return;

            var blockTypeData = await System.IO.File.ReadAllTextAsync("Seeder/Data/blocktypedata.json");
            var blockTypes = JsonSerializer.Deserialize<List<BlockType>>(blockTypeData);

            foreach(var blockType in blockTypes)
            {
                context.BlockTypes.Add(blockType);
            }
            await context.SaveChangesAsync();
        }
    }
}
