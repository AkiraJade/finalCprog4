import greenfoot.*;
public class Platform extends Actor
{
    private int lastX;
    private int lastY;
    private boolean firstAct = true;
    
    public void act()
    {
        if (firstAct)
        {
            lastX = getX();
            lastY = getY();
            firstAct = false;
        }
        else
        {
            pushPerson();
            lastX = getX();
            lastY = getY();
        }
    }
    
    private void pushPerson()
    {
        int deltaX = getX() - lastX;
        int deltaY = getY() - lastY;
        
        if (deltaX == 0 && deltaY == 0)
        {
            return; // Platform didn't move
        }
        
        // Find any person touching this platform
        java.util.List<Person> persons = getIntersectingObjects(Person.class);
        
        for (Person person : persons)
        {
            // Get precise boundaries
            int personBottom = person.getY() + person.getImage().getHeight() / 2;
            int personTop = person.getY() - person.getImage().getHeight() / 2;
            int personLeft = person.getX() - person.getImage().getWidth() / 2;
            int personRight = person.getX() + person.getImage().getWidth() / 2;
            
            int platformTop = getY() - getImage().getHeight() / 2;
            int platformBottom = getY() + getImage().getHeight() / 2;
            int platformLeft = getX() - getImage().getWidth() / 2;
            int platformRight = getX() + getImage().getWidth() / 2;
            
            // Check if person is standing on top of platform
            boolean isOnTop = personBottom >= platformTop - 15 && 
                              personBottom <= platformTop + 5 &&
                              personRight > platformLeft && 
                              personLeft < platformRight;
            
            if (isOnTop)
            {
                // Person is on top of platform
                if (deltaY < 0)
                {
                    // Platform moving up - push person up and horizontally
                    person.setLocation(person.getX() + deltaX, person.getY() + deltaY);
                }
                else if (deltaY > 0)
                {
                    // Platform moving down - only push horizontally, let gravity handle vertical
                    person.setLocation(person.getX() + deltaX, person.getY());
                }
                else
                {
                    // Only horizontal movement - push horizontally
                    person.setLocation(person.getX() + deltaX, person.getY());
                }
            }
            else if (deltaX != 0)
            {
                // Platform moving horizontally - check if person is beside it
                boolean isBeside = personBottom > platformTop && 
                                   personTop < platformBottom;
                
                if (isBeside)
                {
                    // Push person horizontally
                    int newX = person.getX() + deltaX;
                    
                    // Prevent penetration by ensuring person stays outside platform
                    if (deltaX > 0) // Platform moving right
                    {
                        // Push person to the right of platform
                        if (personLeft < platformRight && personRight > platformLeft)
                        {
                            newX = platformRight + person.getImage().getWidth() / 2;
                        }
                    }
                    else if (deltaX < 0) // Platform moving left
                    {
                        // Push person to the left of platform
                        if (personRight > platformLeft && personLeft < platformRight)
                        {
                            newX = platformLeft - person.getImage().getWidth() / 2;
                        }
                    }
                    
                    person.setLocation(newX, person.getY());
                }
            }
            
            // Additional safety: if person is inside platform after movement, push them out
            ensurePersonOutside(person);
        }
    }
    
    private void ensurePersonOutside(Person person)
    {
        int personBottom = person.getY() + person.getImage().getHeight() / 2;
        int personTop = person.getY() - person.getImage().getHeight() / 2;
        int personLeft = person.getX() - person.getImage().getWidth() / 2;
        int personRight = person.getX() + person.getImage().getWidth() / 2;
        
        int platformTop = getY() - getImage().getHeight() / 2;
        int platformBottom = getY() + getImage().getHeight() / 2;
        int platformLeft = getX() - getImage().getWidth() / 2;
        int platformRight = getX() + getImage().getWidth() / 2;
        
        // Check if person is penetrating the platform
        boolean overlapping = personRight > platformLeft && 
                              personLeft < platformRight &&
                              personBottom > platformTop && 
                              personTop < platformBottom;
        
        if (overlapping)
        {
            // Calculate penetration depths
            int penetrationLeft = personRight - platformLeft;
            int penetrationRight = platformRight - personLeft;
            int penetrationTop = personBottom - platformTop;
            int penetrationBottom = platformBottom - personTop;
            
            // Find the smallest penetration and push out in that direction
            int minPenetration = Math.min(Math.min(penetrationLeft, penetrationRight),
                                         Math.min(penetrationTop, penetrationBottom));
            
            if (minPenetration == penetrationTop && personBottom > platformTop)
            {
                // Push person up (they're on top)
                person.setLocation(person.getX(), platformTop - person.getImage().getHeight() / 2);
            }
            else if (minPenetration == penetrationBottom)
            {
                // Push person down (they hit from below)
                person.setLocation(person.getX(), platformBottom + person.getImage().getHeight() / 2);
            }
            else if (minPenetration == penetrationLeft)
            {
                // Push person left
                person.setLocation(platformLeft - person.getImage().getWidth() / 2, person.getY());
            }
            else if (minPenetration == penetrationRight)
            {
                // Push person right
                person.setLocation(platformRight + person.getImage().getWidth() / 2, person.getY());
            }
        }
    }
    
    public Platform(int width, int height)
    {
        GreenfootImage img = new GreenfootImage("peach.png");
        img.scale(width, height);
        setImage(img);
    }
}