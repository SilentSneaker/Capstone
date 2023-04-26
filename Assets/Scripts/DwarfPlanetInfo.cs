using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DwarfPlanetInfo : MonoBehaviour
{
    public string[] dwarfPlanetInfo;

    public DwarfPlanetInfo()
    {
        dwarfPlanetInfo = new string[]
        {
        // Pluto
        "Pluto is a dwarf planet in the Kuiper Belt, a donut-shaped region of icy bodies beyond the orbit of Neptune. There may be millions of these icy objects, collectively referred to as Kuiper Belt objects (KBOs) or trans-Neptunian objects (TNOs), in this distant region of our solar system. Pluto � which is smaller than Earth�s Moon � has a heart-shaped glacier that�s the size of Texas and Oklahoma. This fascinating world has blue skies, spinning moons, mountains as high as the Rockies, and it snows � but the snow is red. On July 14, 2015, NASA�s New Horizons spacecraft made its historic flight through the Pluto system � providing the first close-up images of Pluto and its moons and collecting other data that has transformed our understanding of these mysterious worlds on the solar system�s outer frontier. In the years since that groundbreaking flyby, nearly every conjecture about Pluto possibly being an inert ball of ice has been thrown out the window or flipped on its head. �It�s clear to me that the solar system saved the best for last!� said Alan Stern, New Horizons principal investigator from the Southwest Research Institute, Boulder, Colorado. �We could not have explored a more fascinating or scientifically important planet at the edge of our solar system.The New Horizons team worked for 15 years to plan and execute this flyby and Pluto paid us back in spades!�",
        // Ceres
        "Dwarf planet Ceres is the largest object in the asteroid belt between Mars and Jupiter, and it's the only dwarf planet located in the inner solar system. It was the first member of the asteroid belt to be discovered when Giuseppe Piazzi spotted it in 1801. And when NASA's Dawn arrived in 2015, Ceres became the first dwarf planet to receive a visit from a spacecraft. Called an asteroid for many years, Ceres is so much bigger and so different from its rocky neighbors that scientists classified it as a dwarf planet in 2006.Even though Ceres comprises 25 % of the asteroid belt's total mass, Pluto is still 14 times more massive.",
        // Makemake
        "Along with fellow dwarf planets Pluto, Eris, and Haumea, Makemake is located in the Kuiper Belt, a donut-shaped region of icy bodies beyond the orbit of Neptune. Slightly smaller than Pluto, Makemake is the second-brightest object in the Kuiper Belt as seen from Earth (while Pluto is the brightest). It takes about 305 Earth years for this dwarf planet to make one trip around the Sun. Makemake holds an important place in the history of solar system studies because it � along with Eris � was one of the objects whose discovery prompted the International Astronomical Union to reconsider the definition of a planet and to create the new group of dwarf planets. Makemake was first observed in March 2005 by M.E. Brown, C.A. Trujillo, and D.L. Rabinowitz at the Palomar Observatory. Its unofficial codename was Easterbunny. Before this dwarf planet was confirmed, its provisional name was 2005 FY9. In 2016, NASA�s Hubble Space Telescope spotted a small, dark moon orbiting Makemake.",
        // Haumea
        "Haumea is roughly the same size as Pluto. It is one of the fastest rotating large objects in our solar system. The fast spin distorts Haumea's shape, making this dwarf planet look like a football. Two teams claim credit for discovering Haumea citing evidence from observations made in 2003 and 2004.The International Astronomical Union's Gazetteer of Planetary Nomenclature lists the discovery location as Sierra Nevada Observatory in Spain on Mar. 7, 2003, but no official discoverer is listed. Everything we know about Haumea is from observations with ground - based telescopes from around the world.",
        // Eris
        "Eris is one of the largest known dwarf planets in our solar system. It's about the same size as Pluto but is three times farther from the Sun. At first, Eris appeared to be larger than Pluto. This triggered a debate in the scientific community that led to the International Astronomical Union's decision in 2006 to clarify the definition of a planet. Pluto, Eris, and other similar objects are now classified as dwarf planets. Eris was discovered on Jan. 5, 2005, from data obtained on Oct. 21, 2003 during a Palomar Observatory survey of the outer solar system by Mike Brown, a professor of planetary astronomy at the California Institute of Technology; Chad Trujillo of the Gemini Observatory; and David Rabinowitz of Yale University."
        };
    }

    public string GetInfo(int index)
    {
        return dwarfPlanetInfo[index];
    }
}
