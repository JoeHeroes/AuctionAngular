﻿namespace AuctionAngular.Dtos.Event
{
    public class CreateEventDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Color { get; set; }
        public bool AllDay { get; set; }
        public int Owner { get; set; }
    }
}
