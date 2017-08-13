using Echo.Maps;
using Echo.Queues;
using Echo.Waves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Echo.Sample.GUI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int Columns = 50;
        private const int Rows = Columns;
        private const int CellWidth = 40;
        private const int CellHeight = CellWidth;

        private readonly Brush _normalBrush;
        private readonly Brush _obstacleBrush;
        private readonly Brush _waypointBrush;
        private readonly Brush _startBrush;
        private readonly Brush _finishBrush;

        private IControlStrategy _strategy;

        private readonly bool[,] _table = new bool[Columns, Rows];

        private readonly HashSet<Vector3> _start = new HashSet<Vector3>();
        private readonly HashSet<Vector3> _finish = new HashSet<Vector3>();



        public MainWindow()
        {
            InitializeComponent();

            _normalBrush = (Resources["NormalBrush"] as Brush) ?? new SolidColorBrush(Colors.LightGreen);
            _obstacleBrush = (Resources["ObstacleBrush"] as Brush) ?? new SolidColorBrush(Colors.Gray);
            _waypointBrush = (Resources["WaypointBrush"] as Brush) ?? new SolidColorBrush(Colors.LightBlue);
            _startBrush = (Resources["StartBrush"] as Brush) ?? new SolidColorBrush(Colors.DarkBlue);
            _finishBrush = (Resources["FinishBrush"] as Brush) ?? new SolidColorBrush(Colors.Blue);

            _strategy = new MapEditorStrategy(this);

            MapGrid.Width = Columns * CellWidth;
            MapGrid.Height = Rows * CellHeight;

            for (int x = 0; x < Columns; x++)
            {
                MapGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int y = 0; y < Rows; y++)
            {
                MapGrid.RowDefinitions.Add(new RowDefinition());
            }

            for (int x = 0; x < Columns; x++)
            {
                for (int y = 0; y < Rows; y++)
                {
                    _table[x, y] = true;
                    var control = new Rectangle { Fill = _normalBrush, Focusable = false };
                    control.MouseDown += OnClick;
                    Grid.SetColumn(control, x);
                    Grid.SetRow(control, y);
                    MapGrid.Children.Add(control);
                }
            }

            KeyDown += OnKeyDown;
        }



        private void BeginSearch()
        {
            if (_finish.Count != 1)
            {
                MessageBox.Show("Only single destination point is currently supported.");
                return;
            }

            if (_start.Count < 1)
            {
                MessageBox.Show("At least one starting point should be provided.");
                return;
            }

            var strategy = _strategy;
            _strategy = new IgnoreStrategy();

            foreach (var control in MapGrid.Children.Cast<Shape>().Where(c => c.Fill == _waypointBrush))
            {
                control.Fill = _normalBrush;
            }

            var tracer = new Tracer<Wave>();
            var map = new GridMap(_table);
            var destination = _finish.First();
            foreach (var location in _start)
            {
                var route = tracer.FindShortestPath(location, destination, map);
                if (route == null) continue;
                foreach (var waypoint in route.Skip(1).Take(route.Count - 2))
                {
                    var control = MapGrid.Children[(int)(waypoint.X*Columns + waypoint.Y)] as Shape;
                    control.Fill = _waypointBrush;
                }
            }

            _strategy = strategy;
        }



        private void Reset()
        {
            foreach (var element in MapGrid.Children)
            {
                Shape control; int x, y;
                if (!RetrieveParameters(element, out control, out x, out y))
                    continue;
                _table[x, y] = true;
                _start.Clear();
                _finish.Clear();
                control.Fill = _normalBrush;
            }
        }



        public void SwitchControl()
        {
            if (_strategy is MapEditorStrategy)
                _strategy = new LocationsEditorStrategy(this);
            else if (_strategy is LocationsEditorStrategy)
                _strategy = new MapEditorStrategy(this);
        }



        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter: Reset(); break;

                case Key.Space: BeginSearch(); break;

                case Key.Tab: SwitchControl(); break;

                case Key.Escape: Application.Current.Shutdown(); break;
            }
        }



        private void OnClick(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed &&
                e.RightButton == MouseButtonState.Released)
            {
                _strategy.MouseLeftButtonDown(sender);
                return;
            }

            if (e.LeftButton == MouseButtonState.Released &&
                e.RightButton == MouseButtonState.Pressed)
            {
                _strategy.MouseRightButtondown(sender);
                return;
            }
        }
    }
}
