using NDesk.Options;
using System;
using System.Collections.Generic;

namespace NDeskOptionsSample
{
    internal enum DeviceOperation
    {
        ENTER_DFU=0,
        EXIT_DFU,
        RECONNECT,
        GETSTATUS,
        GETSTATE
    }

    class Program
    {
        private List<DeviceOperation> _requestedOperations;
        internal int ProductIdDfu { get; private set; }
        internal int ProductId { get; private set; }
        internal int VendorId { get; private set; }
        internal bool Help { get; private set; } = false;

        internal bool DeviceInfoProvided { get; private set; } = false;
        internal IReadOnlyCollection<DeviceOperation> RequestedOperations => _requestedOperations;

        internal Program()
        {
            _requestedOperations = new List<DeviceOperation>();
        }

        private void ShowHelp(OptionSet p)
        {
            Console.WriteLine("Usage: dfu-test DEVICE OPERATION(S)");
            Console.WriteLine("Application to test DFU.");
            Console.WriteLine();
            Console.WriteLine("OPERATION:");
            p.WriteOptionDescriptions(Console.Out);
        }

        internal bool ValidateOption()
        {
            throw new NotImplementedException();
        }

        internal OptionSet CreateOptions()
        {
            var optSet = new OptionSet() {
            { "d|device=", "Device Parameters VALUE as vendorId:productId:productIdDfu [MANDATTORY]",
                deviceArgs => {
                    if (deviceArgs == null)
                        throw new OptionException ("Missing device info for option -D.",
                                "-D");
                    var deviceParams = deviceArgs.Split(':');
                    if(deviceParams.Length!=3)
                        throw new OptionException ("Option -D takes parameters as " +
                            "<VendorId>:<ProductId>:<ProductIdDfu>",
                                "-D");
                    try
                    {
                        VendorId = Convert.ToInt32(deviceParams[0], 16);
                        ProductId = Convert.ToInt32(deviceParams[1], 16);
                        ProductIdDfu = Convert.ToInt32(deviceParams[2], 16);
                    }
                    catch(Exception)
                    {
                        throw new OptionException ("Option -D takes parameters as " +
                            "<VendorId>:<ProductId>:<ProductIdDfu> in HEX format",
                                "-D");
                    }

                    DeviceInfoProvided = true;
                } },
            { "G|get_status",  "Get Device Status",
              v =>
              {
                  _requestedOperations.Add(DeviceOperation.GETSTATUS);
              } },
            { "g|get_state",  "Get Device State",
              v =>
              {
                  _requestedOperations.Add(DeviceOperation.GETSTATE);
              } },
            { "r|reconnect",  "Reconnect device",
              v =>
              {
                  _requestedOperations.Add(DeviceOperation.RECONNECT);
              } },
            { "E|exit_dfu",  "Exit DFU mode",
              v =>
              {
                  _requestedOperations.Add(DeviceOperation.EXIT_DFU);
              } },
            { "e|enter_dfu",  "Enter DFU mode",
              v =>
              {
                  _requestedOperations.Add(DeviceOperation.ENTER_DFU);
              } },
            { "h|help",  "show this message and exit",
              v => Help = v != null },
            };

            return optSet;
        }

        static void Main(string[] args)
        {
            var program = new Program();
            var optionSet = program.CreateOptions();

            try
            {
                optionSet.Parse(args);
            }
            catch (OptionException e)
            {
                Console.Write("app: ");
                Console.WriteLine(e.Message);
                Console.WriteLine("Try `app --help' for more information.");
                return;
            }

            if (program.Help)
                program.ShowHelp(optionSet);

            if(!program.DeviceInfoProvided)
            {
                if (program.RequestedOperations.Count > 0)
                {
                    Console.WriteLine("Device Info is required!");
                    program.ShowHelp(optionSet);
                }
            }

            if (program.RequestedOperations.Count <= 0)
            {
                Console.WriteLine("Device Operation is required!");
                program.ShowHelp(optionSet);
            }
        }
    }
}