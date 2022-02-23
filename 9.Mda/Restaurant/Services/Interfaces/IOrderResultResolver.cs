using Services.Request;

namespace Services.Interfaces;

public interface IOrderResultResolver
{
    void Handle(ClearBookingAsyncResult result);
    void Handle(ClearBookingResult result);
    void Handle(SetBookingAsyncResult result);
    void Handle(SetBookingResult result);
}
