using Echo.Abstract;
using Echo.Conditions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Waves
{
    /// <summary>
    /// Provides straightforward implementation of <see cref="IWave"/> and <see cref="IWaveBehaviour{TWave}"/> interfaces for subclasses.
    /// </summary>
    /// <typeparam name="TWave">Wave type that derives from this class.</typeparam>
    public abstract class Base<TWave> : IWave, IWaveBehaviour<TWave> 
        where TWave : Base<TWave>
    {
        private TWave[] _progenitors;
        private List<Vector3> _path = new List<Vector3>();

        /// <summary>
        /// Index of the progenitor's <see cref="PathSegment"/> at which this wave was created.
        /// </summary>
        public int OriginIndex { get; private set; }
        /// <summary>
        /// Normalized direction of this wave.
        /// </summary>
        public Vector3 Direction { get; private set; }
        /// <summary>
        /// Current location of this wave.
        /// </summary>
        public Vector3 Location => _path[_path.Count - 1];
        /// <summary>
        /// Location where this wave was created.
        /// </summary>
        public Vector3 Origin => _path[0];
        /// <summary>
        /// List of progenitors. First progenitor in the list is immediate parent of this wave.
        /// Last progenitor in the list is an initial wave.
        /// </summary>
        public IReadOnlyList<TWave> Progenitors => _progenitors;
        /// <summary>
        /// List of waypoints that this wave traversed while moving from its <see cref="Origin"/> location.
        /// </summary>
        public IReadOnlyList<Vector3> PathSegment => _path;
        
        /// <summary>
        /// Reconstructs full path from this wave and its progenitors.
        /// </summary>
        public Vector3[] FullPath
        {
            get
            {
                var count = _path.Count + OriginIndex + 1;
                for (int i = 0; i < _progenitors.Length; i++)
                {
                    count += _progenitors[i].OriginIndex;
                }
                var output = new Vector3[count];

                _path.CopyTo(output, output.Length - _path.Count);
                for (int i = 0, at = output.Length - _path.Count, origin = OriginIndex; i < _progenitors.Length; i++)
                {
                    var progenitor = _progenitors[i];
                    at -= origin;
                    progenitor._path.CopyTo(0, output, at, origin);
                    origin = progenitor.OriginIndex;
                }
                
                return output;
            }
        }


        /// <summary>
        /// Provides logic necessary to construct <see cref="Waves.Base{TWave}"/> and its subclasses.
        /// </summary>
        public class Builder : IWaveBuilder<TWave>
        {
            /// <summary>
            /// An object to initialize waves' <see cref="IWaveBehaviour{TWave}.AcceptanceCondition"/> property with.
            /// </summary>
            public ICondition<TWave> AcceptanceCondition;
            /// <summary>
            /// An object to initialize waves' <see cref="IWaveBehaviour{TWave}.FadeCondition"/> property with.
            /// </summary>
            public ICondition<TWave> FadeCondition;
            /// <summary>
            /// An object to initialize waves' <see cref="IWaveBehaviour{TWave}.Propagation"/> property with.
            /// </summary>
            public IPropagationStrategy<TWave> PropagationStrategy;
            /// <summary>
            /// An object to initialize waves' <see cref="IWaveBehaviour{TWave}.Update"/> property with.
            /// </summary>
            public IUpdateStrategy<TWave> UpdateStrategy;
            /// <summary>
            /// Builder for custom wave types initialization. Will be used right after this builder finished initialization to ensure overrides.
            /// </summary>
            public IWaveBuilder<TWave> NestedBuilder;

            /// <summary>
            /// Initializes given wave as new one.
            /// </summary>
            /// <param name="wave">Wave to initialize.</param>
            /// <param name="location">Location to place given wave at.</param>
            /// <param name="direction">Direction of the wave.</param>
            public void Build(TWave wave, Vector3 location, Vector3 direction)
            {
                wave.AcceptanceCondition = AcceptanceCondition;
                wave.FadeCondition = FadeCondition;
                wave.Propagation = PropagationStrategy;
                wave.Update = UpdateStrategy;

                wave.Direction = direction;
                wave.OriginIndex = -1;
                wave._path.Add(location);
                wave._progenitors = new TWave[0];

                if (NestedBuilder != null)
                    NestedBuilder.Build(wave, location, direction);

                if (wave.AcceptanceCondition == null)
                {
                    Debug.WriteLine($"ECHO: {this}: Initial acceptance condition is not set for [{wave}]. A wave without such condition will never be yielded during enumeration.");
                }
                if (wave.FadeCondition == null)
                {
                    Debug.WriteLine($"ECHO: {this}: Initial fade condition is not set for [{wave}]. Infinite loops within processing queue may occur.");
                }
                if (wave.Propagation == null)
                {
                    Debug.WriteLine($"ECHO: {this}: Initial propagation strategy is not set for [{wave}]. Without this strategy it will not spawn any additional waves.");
                }
                if (wave.Update == null)
                {
                    Debug.WriteLine($"ECHO: {this}: Initial update strategy is not set for [{wave}]. Provide this strategy if you need to update custom wave data.");
                }
            }

            /// <summary>
            /// Initializes given wave as a progeny.
            /// </summary>
            /// <param name="wave">Wave to initialize.</param>
            /// <param name="progenitor">Wave to inherit properties from.</param>
            /// <param name="direction">Direction of the wave.</param>
            /// <param name="offset">Location offset relative to the progenitor wave.</param>
            public void Build(TWave wave, TWave progenitor, Vector3 direction, Vector3 offset)
            {
                wave.AcceptanceCondition = progenitor.AcceptanceCondition;
                wave.FadeCondition = progenitor.FadeCondition;
                wave.Propagation = progenitor.Propagation;
                wave.Update = progenitor.Update;

                wave.Direction = direction;
                wave.OriginIndex = progenitor._path.Count - 1;
                wave._path.Add(progenitor._path[wave.OriginIndex] + offset);
                wave._progenitors = new TWave[progenitor._progenitors.Length + 1];
                wave._progenitors[0] = progenitor;
                Array.Copy(progenitor._progenitors, 0, wave._progenitors, 1, progenitor._progenitors.Length);

                if (NestedBuilder != null)
                    NestedBuilder.Build(wave, progenitor, direction, offset);
            }
        }

        
        /// <summary>
        /// Tested by tracer when this wave is being processed. Only acceptable waves will be returned by the search routine.
        /// </summary>
        public ICondition<TWave> AcceptanceCondition { get; set; }
        /// <summary>
        /// Tested by tracer when this wave is being processed. Fading waves will be removed from processing queue.
        /// </summary>
        public ICondition<TWave> FadeCondition { get; set; }
        /// <summary>
        /// Executed by tracer when this wave is being processed.
        /// </summary>
        public IPropagationStrategy<TWave> Propagation { get; set; }
        /// <summary>
        /// Executed by tracer 
        /// </summary>
        public IUpdateStrategy<TWave> Update { get; set; }


        /// <summary>
        /// Adds new waypoint to the <see cref="Waves.Base{TWave}.PathSegment"/>.
        /// </summary>
        /// <param name="to">Coordinates to place wave at.</param>
        public void Relocate(Vector3 to)
        {
            _path.Add(to);
        }


        /// <summary>
        /// Provides textual representation of the wave in form TYPE DIRECTION LOCATION.
        /// </summary>
        /// <returns>Textual representation of the wave.</returns>
        public override string ToString()
        {
            return $"{GetType().Name} {Direction} {Location}";
        }
    }
}
