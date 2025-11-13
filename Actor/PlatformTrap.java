import greenfoot.*;  // (World, Actor, GreenfootImage, Greenfoot and MouseInfo)

/**
 * Write a description of class PlaformTrap here.
 * 
 * @author (your name) 
 * @version (a version number or a date)
 */
public class PlatformTrap extends Actor
{
    /**
     * Act - do whatever the PlaformTrap wants to do. This method is called whenever
     * the 'Act' or 'Run' button gets pressed in the environment.
     */
    public void act()
    {
        // Add your action code here.
    }
    
    public PlatformTrap() {
        GreenfootImage img = new GreenfootImage("peach.png");
        img.scale(70,100);
        setImage(img);
    }
}