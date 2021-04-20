using Automapper.Models;
using AutoMapper;
using DataLayer.Entityes;

namespace ChatAPI.Options
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Message, MessageModel>();
        }
    }
}
