# What is it for?
The framework is designed to support development of complicated route-building algorithms required for solving exotic path-finding problems such as "*find a path with given length and number of turns that would start in any point within given area*" or "*find a path that has no more than given number of intersections and goes through given points*" and so on.

# What is it not?
- It is **not** an another A* implementation, although extension methods that search for the shortest paths are available.
- It is **not** a set of production-ready solutions, but rather a tool to build such solutions.

# What does it need?
The framework depends on **System.Numerics.Vectors** and requires **.NET framework 4.5**. It is advised to install it via nuget manager ([Navigation.Echo](https://www.nuget.org/packages/Navigation.Echo/)).

# How to use?
In short, you need to create a `Tracer<TWave>` object  and invoke its `Search` method, or one of the extension methods.

To create a `Tracer<TWave>` object, you must first specify which type of waves will it operate with. There you can use either `Wave` class which is located in `Echo.Waves` namespace, or provide a custom type that implements both `IWave` and `IWaveBehaviour<TWave>` interfaces. It is advised to use `Base<TWave>` abstract class as a base for custom types since it implements necessary interfaces for you.

Before explaining how `Search` method works, an overview of the general concept must be given: tracer propagates (moves and multiplies) wave objects across given map until one of them satisfies set condition. The wave is then yield-returned so that consumer can filter output of the method using LINQ extensions without the need to find ALL suitable waves (possibly infinite in number).

That being said, `Tracer<TWave>.Search` method requires three parameters to operate:

- A strategy that can provide an initial set of waves to propagate.

There is a number of generic strategies in `Echo.InitializationStrategies` namespace that can be used exactly for that. All of them, however, require an implementation of `IWaveBuilder<TWave>` interface to initializae newly created waves. If the wave type you use derives from `Base<TWave>` you must provide `Base<TWave>.Builder` for base class initialization and an additional builder for custom type initialization (if it is necessary).

- A map to navigate waves.

Framework provides two basic implementations of `IMap<TWave>` interface that are located in `Echo.Maps` namespace: `GridMap` and `GraphMap`. First one is easy to use, while second is more flexible. Both maps support generation from 2D or 3D boolean tables.

- A queue to store and sort waves between processing cycles.

Three implementations of the `IProcessingQueue<T>` interface are available in `Echo.Queues` namespace. Two of them are adapters for system classes: `StackAdapter<TWave>` and `QueueAdapter<TWave>`. Using a stack as the queue  will result in depth-first search and using an actual queue will result in bredth-first search. These may be viable options when it is not possible to evaluate how close is a wave to reaching its goal. In other cases `PriorityQueue<T>` supplied with appropritate `IPriorityMeter` implementations would be preferable.
