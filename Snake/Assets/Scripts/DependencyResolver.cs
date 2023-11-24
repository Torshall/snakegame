using System;
using System.Collections.Generic;

/// <summary>
/// Keeps track of all dependecies using the IDependecyInterface.
/// The idea is to make important systems and managers reachable while avoiding injections and singletons as they tend to become problematic as a project grows
/// </summary>
public class DependencyResolver
{
	private static DependencyResolver instance { get; set; }

	private Dictionary<Type, IDependecyInterface> dependencies = new Dictionary<Type, IDependecyInterface>();


	public DependencyResolver()
	{
		if (instance != null)
		{
			throw new ArgumentException("DependencyResolver already created, this should never happen");
		}

		instance = this;
	}

	public static void AddDependecy<T>(T dependecy) where T : IDependecyInterface
	{
		instance.dependencies.Add(typeof(T), dependecy);
	}

	public static T GetDependecy<T>() where T : IDependecyInterface
	{
		if(instance.dependencies.ContainsKey(typeof(T)))
		{
			return (T)instance.dependencies[typeof(T)];
		}

		throw new ArgumentException("Dependecy not found, has it been added?");
	}

}
