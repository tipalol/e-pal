using Epal.Domain.Enums;

namespace Epal.Application.Features.Profile.Models;

 public class ProfileResponse
    {
        public string Email { get; set; }
        public string ProfileId { get; set; }
        public string Username { get; set; }
        public decimal Balance { get; set; }
        public UserStatus UserStatus { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
        public ICollection<Domain.Entities.Profile> Follower { get; set; }
        private ProfileResponse()
        {
        }

        // Внутренний класс-строитель
        public class Builder
        {
            private string _email;
            private string _profileId;
            private decimal _balance;
            private ResponseStatus _responseStatus;
            private UserStatus _profileStatus;
            private List<Domain.Entities.Profile> _follower = new();

            public Builder(Domain.Entities.Profile profile)
            {
                _email = profile.Email;
                _profileId = profile.ProfileId;
                _profileStatus = profile.Status;
                if (profile.Follower is not null)
                foreach (var v in profile.Follower)
                {
                    _follower.Add(v);
                }
                _balance = profile.Balance;
            }

            public Builder WithResponseStatus(ResponseStatus responseStatus)
            {
                _responseStatus = responseStatus;
                return this;
            }

            public ProfileResponse Build()
            {
                return new ProfileResponse
                {
                    Email = _email,
                    ProfileId = _profileId,
                    Balance = _balance,
                    UserStatus = _profileStatus,
                    Follower = _follower,
                    ResponseStatus = _responseStatus
                };
            }
        }

        // Статический метод для создания экземпляра Builder
        public static Builder Create(Domain.Entities.Profile profile)
        {
            return new Builder(profile);
        }
    }
