import greenfoot.*;

public class Shuriken extends Actor
{
    private GreenfootImage[] frames;
    private int currentFrame;
    private int animationDelay;
    private int delayCounter;
    
    public Shuriken()
    {
        // Initialize animation frames
        frames = new GreenfootImage[8];
        for (int i = 0; i < 8; i++)
        {
            frames[i] = new GreenfootImage("shuriken" + (i + 1) + ".png");
            // Scale images to be bigger (1.5x size)
            frames[i].scale(frames[i].getWidth() * 3 / 2, frames[i].getHeight() * 3 / 2);
        }
        
        currentFrame = 0;
        animationDelay = 3; // Change frame every 3 act cycles (adjust for speed)
        delayCounter = 0;
        
        setImage(frames[currentFrame]);
    }
    
    public void act()
    {
        animate();
        // Add movement or other behavior here if needed
        // Example: move(5);
    }
    
    private void animate()
    {
        delayCounter++;
        
        if (delayCounter >= animationDelay)
        {
            currentFrame = (currentFrame + 1) % frames.length;
            setImage(frames[currentFrame]);
            delayCounter = 0;
        }
    }
    
    // Method to change animation speed (lower = faster)
    public void setAnimationSpeed(int delay)
    {
        animationDelay = delay;
    }
}