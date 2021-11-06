using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace DomainCqrs.Pages
{
    public class IndexModel : DomainCqrsPageModel
    {
        public void OnGet()
        {
            
        }

        public async Task OnPostLoginAsync()
        {
            await HttpContext.ChallengeAsync("oidc");
        }
    }
}