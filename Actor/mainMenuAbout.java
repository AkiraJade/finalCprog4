import greenfoot.*;  // (World, Actor, GreenfootImage, Greenfoot and MouseInfo)

/**
 * Write a description of class mainMenuAbout here.
 * 
 * @author (your name) 
 * @version (a version number or a date)
 */
public class mainMenuAbout extends Actor
{
    /**
     * Act - do whatever the mainMenuAbout wants to do. This method is called whenever
     * the 'Act' or 'Run' button gets pressed in the environment.
     */
    public void act()
    {
        GreenfootImage img = getImage(); 
        img.scale(170, 50); 
        setImage(img);
        
        
    }
}


