using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace DiggerSoft
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Digger Analyzer 3000 started!");

            if (args.Length == 1 && args[0] == "help" || args.Length == 0)
            {
                Console.WriteLine("Usage: DiggerSoft [OPTIONS]");
                Console.WriteLine("");
                Console.WriteLine("Option".PadRight(30) + "Description");
                Console.WriteLine(" pic <y|n> <int>".PadRight(30) + "Take pictures and store locally. Use y if picture contains the wanted target");
                Console.WriteLine(" dig [--no-action]".PadRight(30) + "Take picture and initiate digging. If --no-action is specified, only outputs intended action.");
                Console.WriteLine(" help".PadRight(30) + "This help");
                return;
            }

            var p = new Program();

            if (args.Length >= 3)
            {
                if (args[0] == "pic")
                {
                    var folder = "/home/pi/datasets/";
                    switch (args[1])
                    {
                        case "y":
                            folder += "dig";
                            break;
                        case "n":
                            folder += "not-dig";
                            break;
                        default:
                            Console.WriteLine("Argument pic must be followed by 'y' if picture contains a valid target, or 'n' if it does not.");
                            return;
                    }

                    if (int.TryParse(args[2], out var picturesToTake))
                    {
                        for (var i = 0; i < picturesToTake; i++)
                        {
                            var pic = Cam.TakePicture();
                            Cam.StorePicture(folder, pic);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Wtf, you moron. {args[2]} isn't a valid int. Are you even trying?");
                        return;
                    }

                }
            }

            if (args.Length >= 1)
            {
                if (args[0] == "dig")
                {
                    var actionPermitted = true;
                    var moveFactor = 1;
                    if (args.Length == 2)
                    {
                        if (args[1] == "--no-action")
                        {
                            actionPermitted = false;
                        }
                        else if (int.TryParse(args[1], out moveFactor)) { }
                    }

                    var ai = new Ai();
                    var camServo = new CamServo();
                    var moveRobot = new MoveRobot(moveFactor);
                    camServo.SetPuls(1, 500);
                    while (true)
                    {
                        camServo.Turn();
                        var pic = Cam.TakePicture();
                        Cam.StorePicture("/home/pi/datasets/unlabeled", pic);
                        var isValid = await ai.IsImageValidTarget(pic);

                        if (isValid)
                        {
                            Console.WriteLine("Target is identified!");
                            if (actionPermitted)
                            {
                                // Send command about moving
                                if (camServo.GetCurrentPosition() == Position.Left)
                                {
                                    Console.WriteLine("Left");
                                    await moveRobot.Left();
                                    camServo.SetPosition(Position.Left);
                                    //Drive left
                                }
                                else if (camServo.GetCurrentPosition() == Position.Right)
                                {
                                    Console.WriteLine("Right");
                                    await moveRobot.Right();
                                    camServo.SetPosition(Position.Right);
                                    //Drive Right
                                }
                                else
                                {
                                    Console.WriteLine("Center");
                                    await moveRobot.Forward();
                                    // Drive forward
                                }
                            }
                            else
                            {
                                Console.WriteLine("Not allowed to perform further actions.");
                            }
                        }
                        else
                        {
                            camServo.SetNextPosition();
                        }
                    }
                }
            }
        }
    }
}
