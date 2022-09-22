namespace HttpBase.Interfaces;

public interface ICurrentUserService
{
    string UserId { get; }

    string IdToken { get; }

    string UserToken { get; }

    string RefreshToken { get; }

    string Email { get; }

    List<string> Claims { get; }
}
