﻿using System;

namespace AuctionAngular.Dtos
{
    public class EditUserDto
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string SureName { get; set; }
        public string Phone { get; set; }
        public string Nationality { get; set; }
        public DateTime Date { get; set; }
    }
}
