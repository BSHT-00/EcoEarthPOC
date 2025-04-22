using EcoEarthAppAPI.Data;
using EcoEarthAppAPI.Data.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcoEarthAppAPI.Controllers
{
    [Route("api/Quest")]
    [ApiController]
    public class QuestController : ControllerBase
    {
        private readonly EcoEarthAppAPIDbContext _context;

        public QuestController(EcoEarthAppAPIDbContext context)
        {
            _context = context;
        }

        //Gets all quests for a user
        [HttpGet("{userId}/GetAllQuests")]
        public async Task<ActionResult<IEnumerable<Quest>>> GetAllQuests(int userId)
        {
            var user = await _context.Quest.FindAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            var quests = await _context.Quest
                .Where(q => q.UserId == userId)
                .ToListAsync();

            return Ok(quests);
        }

        //gets a specified quest
        [HttpGet("{questId}/GetQuest")]
        public async Task<ActionResult<Quest>> GetQuest(int questId)
        {
            var quest = await _context.Quest.FindAsync(questId);
            if (quest == null)
            {
                return NotFound();
            }
            return Ok(quest);
        }

        //updates a quests progress
        [HttpPut("{questId}/UpdateProgress")]
        public async Task<IActionResult> UpdateProgress(int questId)
        {
            var quest = await _context.Quest.FindAsync(questId);
            if (quest == null)
            {
                return NotFound();
            }
            if (quest.Progress == quest.QuestGoal)
            {
                quest.isDone = true;
                await _context.SaveChangesAsync();
                return Ok(quest);
            }
            else
            {
                quest.Progress++;
                if (quest.Progress == quest.QuestGoal)
                {
                    quest.isDone = true;
                }
                await _context.SaveChangesAsync();
                return Ok(quest);
            }
        }

        //generates and assigns a major quest to a user
        [HttpPost("{userId}/AssignMajorQuest")]
        public async Task<ActionResult> AssignMajorQuest(int userId)
        {
            var user = await _context.Quest.FindAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            //quest generation
            var instruction = new List<string> { "Scan", "Recycle" };
            var category = new List<int> { 1, 2, 3, 4, 5, 6 };
            var random = new Random();

            var quest = new Quest
            {
                UserId = userId,
                QuestType = "Major",
                QuestInstruction = instruction[random.Next(instruction.Count)],
                QuestGoal = 5,
                QuestReward = 10,
                CategoryId = category[random.Next(category.Count)],
                Progress = 0,
                isDone = false,
                LastLoginDate = DateTime.UtcNow
            };

            //assigns quest
            _context.Quest.Add(quest);
            _context.SaveChanges();
            return Ok(quest);
        }

        //generates and assigns a minor quest to a user
        [HttpPost("{userId}/AssignMinorQuest")]
        public async Task<ActionResult> AssignMinorQuest(int userId)
        {
            var user = await _context.Quest.FindAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            //quest generation
            var instruction = new List<string> { "Scan", "Recycle" };
            var category = new List<int> { 1, 2, 3, 4, 5, 6 };
            var random = new Random();

            var quest = new Quest
            {
                UserId = userId,
                QuestType = "Minor",
                QuestInstruction = instruction[random.Next(instruction.Count)],
                QuestGoal = 3,
                QuestReward = 5,
                CategoryId = category[random.Next(category.Count)],
                Progress = 0,
                isDone = false,
                LastLoginDate = DateTime.UtcNow
            };

            _context.Quest.Add(quest);
            _context.SaveChanges();
            return Ok(quest);
        }

        //deletes a users quests
        [HttpDelete("{userId}/DeleteQuests")]
        public async Task<ActionResult> DeleteQuests(int userId)
        {
            var user = await _context.Quest.FindAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            var quests = await _context.Quest
                .Where(q => q.UserId == userId)
                .ToListAsync();

            _context.Quest.RemoveRange(quests);
            await _context.SaveChangesAsync();

            return Ok(quests.Count);
        }
    }
}

