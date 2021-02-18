using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DealerOnExample.Models;

namespace DealerOnExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View("MarsRover", new MarsRoverViewModel());
        }

        public IActionResult MarsRover()
        {
            return View(new MarsRoverViewModel());
        }

        [HttpPost]
        public IActionResult MarsRover(MarsRoverViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            
            //Get rover count by counting the line breaks
            //since each rover has 2 lines of commands
            List<string> commandlines = vm.Command.Split('\n').ToList();
            vm.RoverCount = commandlines.Count / 2;

            //Create rovers per command
            int index = 0;
            foreach (string command in commandlines)
            {
                string tempCommand = command;

                //Create the rover
                if (index % 2 == 0)
                {
                    try
                    {
                        MarsRover marsRover = new MarsRover(index / 2);

                        //Parse rovers X coordinate and validate
                        marsRover.X = Int32.Parse(tempCommand.Substring(0, tempCommand.IndexOf(' ')));
                        tempCommand = tempCommand.Remove(0, tempCommand.IndexOf(' ') + 1);
                        if(marsRover.X > vm.GridSizeX)
                        {
                            ModelState.AddModelError("Command", "Invalid X coordinate for rover.");
                            return View(vm);
                        }

                        //Parse rovers Y coordinate and validate
                        marsRover.Y = Int32.Parse(tempCommand.Substring(0, tempCommand.IndexOf(' ')));
                        tempCommand = tempCommand.Remove(0, tempCommand.IndexOf(' ') + 1);
                        if (marsRover.Y > vm.GridSizeY)
                        {
                            ModelState.AddModelError("Command", "Invalid Y coordinate for rover.");
                            return View(vm);
                        }

                        //Parse rovers Direction and validate
                        marsRover.Direction = char.Parse(tempCommand.Substring(0, 1).ToUpper());
                        if(marsRover.Direction != 'N' && marsRover.Direction != 'E' && marsRover.Direction != 'S' && marsRover.Direction != 'W')
                        {
                            ModelState.AddModelError("Command", "Invalid direction for rover.");
                            return View(vm);
                        }
                        vm.MarsRovers.Add(marsRover);
                    }
                    catch
                    {
                        ModelState.AddModelError("Command", "Oops! Something in your rover initialization(s) wasn't recognized. Please review and try again.");
                        return View(vm);
                    }
                }
                //Send rovers movement commands
                else
                {
                    while(tempCommand.Length > 0)
                    {
                        char commandLetter;
                        switch(commandLetter = char.Parse(tempCommand.Substring(0,1).ToUpper()))
                        {
                            case 'R':
                                vm.MarsRovers.LastOrDefault().rotateRight();
                                break;
                            case 'L':
                                vm.MarsRovers.LastOrDefault().rotateLeft();
                                break;
                            case 'M':
                                vm.MarsRovers.LastOrDefault().moveForward();
                                if(vm.MarsRovers.LastOrDefault().X > vm.GridSizeX || vm.MarsRovers.LastOrDefault().Y > vm.GridSizeY)
                                {
                                    ModelState.AddModelError("Command", "Oops! One of your rovers went off Grid! Please review and try again.");
                                    vm.MarsRovers.Clear();
                                    return View(vm);
                                }
                                break;
                            case '\r':
                                break;
                            default:
                                ModelState.AddModelError("Command", "Oops! Something in your rover command(s) wasn't recognized. Please review and try again.");
                                vm.MarsRovers.Clear();
                                return View(vm);
                        }
                        tempCommand = tempCommand.Remove(0,1);
                    }
                }
                index++;
            }
            return View("MarsRover", vm);
        }
        public IActionResult AddMarsRover(MarsRover rover)
        {
            MarsRoverViewModel vm = new MarsRoverViewModel(rover);
            return View("MarsRover", vm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
