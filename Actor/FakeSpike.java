import greenfoot.*;  // (World, Actor, GreenfootImage, Greenfoot and MouseInfo)

/**
 * Write a description of class FakeSpike here.
 * 
 * @author (your name) 
 * @version (a version number or a date)
 */
public class FakeSpike extends Actor
{
    /**
     * Act - do whatever the FakeSpike wants to do. This method is called whenever
     * the 'Act' or 'Run' button gets pressed in the environment.
     */
    public void act()
    {
        // Add your action code here.
    }
    
     public FakeSpike(){
        GreenfootImage img = new GreenfootImage("spikeF.png");
        img.scale(30,15);
        setImage(img);
    }
    
}