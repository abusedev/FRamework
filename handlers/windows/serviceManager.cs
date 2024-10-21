using System.ServiceProcess;

namespace FRamework.handlers.windows
{
    internal class serviceManager
    {
        public static void stopService(string serviceName)
        {
            ServiceController serviceController = new ServiceController(serviceName);
            if ((serviceController.Status.Equals(ServiceControllerStatus.Running)) || (serviceController.Status.Equals(ServiceControllerStatus.StartPending)) || (serviceController.Status.Equals(ServiceControllerStatus.StopPending)))
            {
                serviceController.Stop();
            }
        }

        public static void restartService(string serviceName)
        {
            ServiceController serviceController = new ServiceController(serviceName);
            if ((serviceController.Status.Equals(ServiceControllerStatus.Running)) || (serviceController.Status.Equals(ServiceControllerStatus.StartPending)) || (serviceController.Status.Equals(ServiceControllerStatus.StopPending)))
            {
                serviceController.Stop();
            }
            serviceController.WaitForStatus(ServiceControllerStatus.Stopped);
            serviceController.Start();
            serviceController.WaitForStatus(ServiceControllerStatus.Running);
        }
    }
}