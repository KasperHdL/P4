using UnityEngine;
using System.Collections;

public struct Settings {

#region General
	public static readonly int 			BODY_POSITION_LENGTH 	= 1000;
	public static readonly int 			DOT_OFFSET 				= 0;
	public static readonly float 		GRAVITATIONAL_CONSTANT 	= 5f;
#endregion

	public struct Planet{
		public static readonly ActiveSliders SLIDERS = ActiveSliders.Mass | ActiveSliders.Radius;

		public static readonly float 	RADIUS_MIN_VALUE 		= 1;	// Metres
		public static readonly float 	RADIUS_MAX_VALUE 		= 100; // Metres
		public static readonly string 	RADIUS_UNIT 			= "Earths"; // Metres

		public static readonly float 	MASS_MIN_VALUE 			= 1; // kg
		public static readonly float 	MASS_MAX_VALUE 			= 100; // kg
	}


	public struct Star{
		public struct Dwarf{

			public static readonly float 	RADIUS_MIN_VALUE 	= 0.1f;
			public static readonly float 	RADIUS_MAX_VALUE 	= 90f;
			public static readonly string 	RADIUS_UNIT 		= "Suns"; // Metres


			public static readonly ActiveSliders SLIDERS = ActiveSliders.Temperature | ActiveSliders.Radius;


			public static readonly char[] CLASSIFICATION = {
				'O',
				'B',
				'A',
				'F',
				'G',
				'K',
				'M'
			};

			public static readonly int[] TEMPERATURE = {
				2400,
				3700,
				5200,
				6000,
				7500,
				10000,
				30000,
				60000
			};

			public static readonly Color[] COLORS = {
				new Color(255f/255,189f/255,111f/255),
				new Color(255f/255,189f/255,111f/255),
				new Color(255f/255,221f/255,180f/255),
				new Color(255f/255,244f/255,232f/255),
				new Color(251f/255,248f/255,255f/255),
				new Color(202f/255,216f/255,255f/255),
				new Color(170f/255,191f/255,255f/255),
				new Color(155f/255,176f/255,255f/255)
			};
		}
	}

}

[System.Flags]
public enum ActiveSliders{
	None 		= 1 << 0,
	Mass 		= 1 << 1,
	Radius 		= 1 << 2,
	Density 	= 1 << 3,
	Temperature = 1 << 4
}
