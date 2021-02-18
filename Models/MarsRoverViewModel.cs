using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DealerOnExample.Models
{
    public class MarsRoverViewModel
    {
        public MarsRoverViewModel()
        {
            GridSizeX = 10;
            GridSizeY = 10;
            RoverCount = 0;
            Command = "";
        }
        public MarsRoverViewModel(MarsRover rover)
        {
            MarsRovers.Add(rover);
            RoverCount++;
        }
        public List<MarsRover> MarsRovers = new List<MarsRover>();
        [Range(1,int.MaxValue, ErrorMessage = "Positives only")]
        public int GridSizeX { get; set; }
        [Range(1,int.MaxValue, ErrorMessage = "Positives only")]
        public int GridSizeY { get; set; }
        public int RoverCount { get; set; }
        [Required(ErrorMessage = "Please enter command(s).")]
        public string Command { get; set; }
    }
}
