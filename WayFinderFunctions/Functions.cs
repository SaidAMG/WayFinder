using WayFinder.Extensions;

namespace WayFinder.WayFinderFunctions
{
    public class Functions
    {


        public static string DescriptionMaker(List<List<int>> _stacklist)
        {
            List<string> steps = new List<string>();
            string advance = "Loop rechtdoor.\n";
            string right = "Ga naar rechts.\n";
            string left = "Ga naar links.\n";
            
            for (int step = 0; step < _stacklist.Count() - 1; step++)
            {
                if (_stacklist[step].SequenceEqual(_stacklist[0]))
                {
                    steps.Add("Loop vanuit locatie A de gang op.\n".ConvertToLineBreaks());
                }
                else
                {
                    int[] North = { _stacklist[step][0] - 1, _stacklist[step][1] };
                    int[] East = { _stacklist[step][0], _stacklist[step][1] + 1 };
                    int[] South = { _stacklist[step][0] + 1, _stacklist[step][1] };
                    int[] West = { _stacklist[step][0], _stacklist[step][1] - 1 };

                    if (North.SequenceEqual(_stacklist[step - 1]))
                    {
                        if (East.SequenceEqual(_stacklist[step + 1]))
                        {
                            steps.Add(left);
                        }

                        if (South.SequenceEqual(_stacklist[step + 1]))
                        {
                            steps.Add(advance);
                        }

                        if (West.SequenceEqual(_stacklist[step + 1]))
                        {
                            steps.Add(right);
                        }
                    }

                    if (East.SequenceEqual(_stacklist[step - 1]))
                    {
                        if (South.SequenceEqual(_stacklist[step + 1]))
                        {
                            steps.Add(left);
                        }

                        if (West.SequenceEqual(_stacklist[step + 1]))
                        {
                            steps.Add(advance);
                        }

                        if (North.SequenceEqual(_stacklist[step + 1]))
                        {
                            steps.Add(right);
                        }
                    }

                    if (South.SequenceEqual(_stacklist[step - 1]))
                    {
                        if (West.SequenceEqual(_stacklist[step + 1]))
                        {
                            steps.Add(left);
                        }

                        if (North.SequenceEqual(_stacklist[step + 1]))
                        {
                            steps.Add(advance);
                        }

                        if (East.SequenceEqual(_stacklist[step + 1]))
                        {
                            steps.Add(right);
                        }
                    }

                    if (West.SequenceEqual(_stacklist[step - 1]))
                    {
                        if (North.SequenceEqual(_stacklist[step + 1]))
                        {
                            steps.Add(left);
                        }

                        if (East.SequenceEqual(_stacklist[step + 1]))
                        {
                            steps.Add(advance);
                        }

                        if (South.SequenceEqual(_stacklist[step + 1]))
                        {
                            steps.Add(right);
                        }
                    }
                }
            }


            steps.Add("U heeft uw bestemming bereikt!\n");

            string Description = steps[0];
            
            for (int step = 1; step < steps.Count() - 1; step++)
            {
                if (steps[step] == steps[step + 1])
                {
                    int totalSteps = 0;
                    int nextStepIndex = step;
                    string nextStep = steps[step];
                    while (steps[step] == nextStep)
                    {
                        totalSteps++;
                        nextStepIndex++;
                        nextStep = steps[nextStepIndex];
                    }

                    step = step + totalSteps - 1;
                    if (steps[step] == "Loop rechtdoor.\n")
                    {
                        Description = Description + $"Loop {totalSteps} stappen vooruit.\n";
                    }
                }
                else
                {
                    Description = Description + steps[step];
                }
            }

            Description = Description + steps[steps.Count() - 1];


            return Description;
        }

        public static int[] NextStep(int[] _currentPosition, List<List<int>> _visited, int[] _start, int[] _end,string[,] _floorplan)
        {
            bool NextOption = false;
            int[] next = new int[2];

            string N = _floorplan[_currentPosition[0] - 1, _currentPosition[1]];
            string E = _floorplan[_currentPosition[0], _currentPosition[1] + 1];
            string S = _floorplan[_currentPosition[0] + 1, _currentPosition[1]];
            string W = _floorplan[_currentPosition[0], _currentPosition[1] - 1];

            //North
            switch (N)
            {
                // Muren
                case "-":
                    NextOption = true;
                    break;

                case "+":
                    NextOption = true;
                    break;

                case "|":
                    NextOption = true;
                    break;

                // Pad
                case " ":
                    int[] North = { _currentPosition[0] - 1, _currentPosition[1] };
                    foreach (List<int> i in _visited)
                    {
                        if (i.SequenceEqual(North.ToList()))
                        {
                            NextOption = true;
                        }
                        else
                        {
                            next[0] = North[0];
                            next[1] = North[1];
                        }
                    }

                    break;

                // Locatie
                case "X":
                    int[] location = { _currentPosition[0] - 1, _currentPosition[1] };
                    if (location.SequenceEqual(_end))
                    {
                        if (_currentPosition.SequenceEqual(_start))
                        {
                            NextOption = true;
                        }
                        else
                        {
                            next[0] = location[0];
                            next[1] = location[1];
                        }
                    }
                    else
                    {
                        NextOption = true;
                    }

                    break;
            }

            if (NextOption)
            {
                NextOption = false;
                //East
                switch (E)
                {
                    // Muren
                    case "-":
                        NextOption = true;
                        break;

                    case "+":
                        NextOption = true;
                        break;

                    case "|":
                        NextOption = true;
                        break;

                    // Pad
                    case " ":
                        int[] East = { _currentPosition[0], _currentPosition[1] + 1 };
                        foreach (List<int> i in _visited)
                        {
                            if (i.SequenceEqual(East.ToList()))
                            {
                                NextOption = true;
                            }
                            else
                            {
                                next[0] = East[0];
                                next[1] = East[1];
                            }
                        }

                        break;

                    // Locatie
                    case "X":
                        int[] location = { _currentPosition[0], _currentPosition[1] + 1 };
                        if (location.SequenceEqual(_end))
                        {
                            if (_currentPosition.SequenceEqual(_start))
                            {
                                NextOption = true;
                            }
                            else
                            {
                                next[0] = location[0];
                                next[1] = location[1];
                            }
                        }
                        else
                        {
                            NextOption = true;
                        }

                        break;
                }
            }

            if (NextOption)
            {
                NextOption = false;
                //South
                switch (S)
                {
                    // Muren
                    case "-":
                        NextOption = true;
                        break;

                    case "+":
                        NextOption = true;
                        break;

                    case "|":
                        NextOption = true;
                        break;

                    // Pad
                    case " ":
                        int[] South = { _currentPosition[0] + 1, _currentPosition[1] };
                        foreach (List<int> i in _visited)
                        {
                            if (i.SequenceEqual(South.ToList()))
                            {
                                NextOption = true;
                            }
                            else
                            {
                                next[0] = South[0];
                                next[1] = South[1];
                            }
                        }

                        if (_visited.Count() > 1)
                        {
                            if (next[0] == _visited[_visited.Count() - 2].ToArray()[0] &&
                                next[1] == _visited[_visited.Count() - 2].ToArray()[1])
                            {
                                next[0] = _currentPosition[0] - 1;
                                next[1] = _currentPosition[1];
                            }

                            if (_floorplan[next[0], next[1]] == "x" || _floorplan[next[0], next[1]] == "+" ||
                                _floorplan[next[0], next[1]] == "-" || _floorplan[next[0], next[1]] == "|")
                            {
                                next[0] = _currentPosition[0];
                                next[1] = _currentPosition[1] - 1;
                            }

                            if (_floorplan[next[0], next[1]] == "x" || _floorplan[next[0], next[1]] == "+" ||
                                _floorplan[next[0], next[1]] == "-" || _floorplan[next[0], next[1]] == "|")
                            {
                                next[0] = _currentPosition[0] + 1;
                                next[1] = _currentPosition[1];
                            }
                        }

                        break;

                    // Locatie
                    case "X":
                        int[] location = { _currentPosition[0] + 1, _currentPosition[1] };
                        if (location.SequenceEqual(_end))
                        {
                            if (_currentPosition.SequenceEqual(_start))
                            {
                                NextOption = true;
                            }
                            else
                            {
                                next[0] = location[0];
                                next[1] = location[1];
                            }
                        }
                        else
                        {
                            NextOption = true;
                        }

                        break;
                }
            }

            if (NextOption)
            {
                NextOption = false;
                //West
                switch (W)
                {
                    // Muren
                    case "-":
                        NextOption = true;
                        break;

                    case "+":
                        NextOption = true;
                        break;

                    case "|":
                        NextOption = true;
                        break;


                    // Pad
                    case " ":
                        int[] West = { _currentPosition[0], _currentPosition[1] - 1 };
                        foreach (List<int> i in _visited)
                        {
                            if (i.SequenceEqual(West.ToList()))
                            {
                                NextOption = true;
                            }
                            else
                            {
                                next[0] = West[0];
                                next[1] = West[1];
                            }
                        }

                        if (_visited.Count() > 1)
                        {
                            if (next[0] == _visited[_visited.Count() - 2].ToArray()[0] &&
                                next[1] == _visited[_visited.Count() - 2].ToArray()[1])
                            {
                                next[0] = _currentPosition[0];
                                next[1] = _currentPosition[1] + 1;
                            }

                            if (_floorplan[next[0], next[1]] == "X" || _floorplan[next[0], next[1]] == "+" ||
                                _floorplan[next[0], next[1]] == "-" || _floorplan[next[0], next[1]] == "|")
                            {
                                next[0] = _currentPosition[0] - 1;
                                next[1] = _currentPosition[1];
                            }

                            if (_floorplan[next[0], next[1]] == "X" || _floorplan[next[0], next[1]] == "+" ||
                                _floorplan[next[0], next[1]] == "-" || _floorplan[next[0], next[1]] == "|")
                            {
                                next[0] = _currentPosition[0];
                                next[1] = _currentPosition[1] - 1;
                            }

                            if (_floorplan[next[0], next[1]] == "X" || _floorplan[next[0], next[1]] == "+" ||
                                _floorplan[next[0], next[1]] == "-" || _floorplan[next[0], next[1]] == "|")
                            {
                                next[0] = _currentPosition[0] + 1;
                                next[1] = _currentPosition[1];
                            }
                        }

                        break;

                    // Locatie
                    case "X":
                        int[] location = { _currentPosition[0], _currentPosition[1] - 1 };
                        if (location.SequenceEqual(_end))
                        {
                            if (_currentPosition.SequenceEqual(_start))
                            {
                                NextOption = true;
                            }
                            else
                            {
                                next[0] = location[0];
                                next[1] = location[1];
                            }
                        }

                        break;
                }
            }

            return next;
        }

        public static string Navigation(int[] _start, int[] _end)
        {
            //Startpunt
            int[] start = _start;
            // Doel
            int[] end = _end;
            // Huidige positie
            int[] currentPosition = start;
            // plekken die al bezocht zijn.
            List<List<int>> stacklist = new List<List<int>>();
            List<List<int>> visited = new List<List<int>>();
            visited.Add(start.ToList());
            stacklist.Add(start.ToList());
            string Description = "Not Found!";

            string[,] floorplan =
            {
                //X coördinaten
                //{"0","1","2","3","4","5","6","7","8","9","10"}
                { "+", "-", "-", "+", "-", "-", "-", "+", "-", "-", "+" }, //0 Y coördinaten
                { "|", "X", " ", "|", "X", "X", "X", "|", " ", "X", "|" }, //1
                { "|", "X", " ", " ", " ", " ", " ", " ", " ", "X", "|" }, //2
                { "|", "X", " ", "|", "X", "X", "X", "|", " ", "X", "|" }, //3
                { "|", "X", " ", "|", "-", "-", "-", "|", " ", "X", "|" }, //4
                { "|", "X", " ", "|", "X", "X", "X", "|", " ", "X", "|" }, //5
                { "|", "X", " ", " ", " ", " ", " ", " ", " ", "X", "|" }, //6
                { "+", "-", "-", "-", "-", "-", "-", "-", "-", "-", "+" } //7
            };

            while (currentPosition[0] != end[0] || currentPosition[1] != end[1])
            {
                bool InStack = false;
                int[] next = Functions.NextStep(currentPosition, visited, start, end, floorplan);
                currentPosition = next;
                visited.Add(next.ToList());

                foreach (List<int> i in stacklist)
                {
                    if (i.SequenceEqual(next.ToList()))
                    {
                        InStack = true;
                    }
                }

                if (InStack == false)
                {
                    stacklist.Add(next.ToList());
                }

                if (InStack)
                {
                    while (!next.SequenceEqual(stacklist.Last().ToArray()))
                    {
                        stacklist.RemoveAt(stacklist.Count() - 1);
                    }

                    InStack = false;
                }
            }

            Description = Functions.DescriptionMaker(stacklist);

            return Description;
        }
    }
}
