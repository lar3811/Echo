using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Echo.Sample.GUI
{
    public partial class MainWindow
    {
        private bool RetrieveParameters<TControl>(object element, out TControl control, out int x, out int y)
            where TControl : FrameworkElement
        {
            x = y = -1;
            control = element as TControl;
            if (control == null || control.Parent == null || !(control.Parent is Grid)) return false;
            x = Grid.GetColumn(control);
            y = Grid.GetRow(control);
            return true;
        }



        private interface IControlStrategy
        {
            void MouseLeftButtonDown(object element);
            void MouseRightButtondown(object element);
        }



        private class IgnoreStrategy : IControlStrategy
        {
            public void MouseLeftButtonDown(object element) { }

            public void MouseRightButtondown(object element) { }
        }



        private class MapEditorStrategy : IControlStrategy
        {
            private readonly MainWindow _window;

            public MapEditorStrategy(MainWindow window)
            {
                _window = window;
            }

            public void MouseLeftButtonDown(object element)
            {
                Shape control; int x, y;
                if (!_window.RetrieveParameters(element, out control, out x, out y)) return;
                var vector = new Vector3(x, y, 0);
                _window._table[x, y] = !_window._table[x, y];
                _window._start.Remove(vector);
                _window._finish.Remove(vector);
                control.Fill = _window._table[x, y]
                    ? _window._normalBrush
                    : _window._obstacleBrush;
            }

            public void MouseRightButtondown(object element)
            {
                MouseLeftButtonDown(element);
            }
        }



        private class LocationsEditorStrategy : IControlStrategy
        {
            private readonly MainWindow _window;

            public LocationsEditorStrategy(MainWindow window)
            {
                _window = window;
            }

            public void MouseLeftButtonDown(object element)
            {
                Shape control; int x, y;
                if (!_window.RetrieveParameters(element, out control, out x, out y)) return;
                var vector = new Vector3(x, y, 0);
                if (_window._start.Remove(vector))
                {
                    control.Fill = _window._normalBrush;
                }
                else
                {
                    _window._start.Add(vector);
                    _window._finish.Remove(vector);
                    control.Fill = _window._startBrush;
                }
                _window._table[x, y] = true;
            }

            public void MouseRightButtondown(object element)
            {
                Shape control; int x, y;
                if (!_window.RetrieveParameters(element, out control, out x, out y)) return;
                var vector = new Vector3(x, y, 0);
                if (_window._finish.Remove(vector))
                {
                    control.Fill = _window._normalBrush;
                }
                else
                {
                    _window._start.Remove(vector);
                    _window._finish.Add(vector);
                    control.Fill = _window._finishBrush;
                }
                _window._table[x, y] = true;
            }
        }
    }
}
