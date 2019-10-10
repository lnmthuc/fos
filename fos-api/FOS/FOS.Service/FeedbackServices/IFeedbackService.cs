using FOS.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOS.Services.FeedbackServices
{
    public interface IFeedbackService
    {
        Model.Domain.FeedBack GetFeedbackByDeliveryId(string DeliveryId);
        List<Model.Domain.FeedBack> GetByFoodId(string foodId);
        void RateRestaurant(Model.Domain.FeedBack feedBack);
    }
}
