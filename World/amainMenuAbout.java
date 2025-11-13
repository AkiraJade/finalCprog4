import greenfoot.*;  // (World, Actor, GreenfootImage, Greenfoot and MouseInfo)

/**
 * Write a description of class amainMenuAbout here.
 * 
 * @author (your name) 
 * @version (a version number or a date)
 */
public class amainMenuAbout extends World
{

    /**
     * Constructor for objects of class amainMenuAbout.
     * 
     */
    public amainMenuAbout()
    {    
        super(750, 400, 1); 
        
        GreenfootImage bg = new GreenfootImage("mainMenuAbout.png"); 
        bg.scale(750, 400);
        setBackground(bg);
        
        backButton backBtn = new backButton();
        addObject(backBtn, 50, 380);
    }
}
