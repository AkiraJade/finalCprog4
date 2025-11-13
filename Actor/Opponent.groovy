import greenfoot.*;

/**
 * Opponent character - same as Person but without keyboard controls
 */
public class Opponent extends Actor
{
    // Animation variables
    private GreenfootImage[] idleFrames;
    private GreenfootImage[] runFrames;
    private GreenfootImage[] jumpFrames;
    private int frameIndex = 0;
    private int delayCounter = 0;
    private int animationDelay = 5;
    private boolean facingRight = true;
    private boolean isRunning = false;
    
    // Physics variables
    private boolean onGround = false;
    private int jumpStrength = -15;
    private int gravity = 1;
    private int vSpeed = 0;
    private int moveSpeed = 3;
    private int airSpeed = 2;
    
    // Platform tracking for moving platforms
    private Platform lastPlatform = null;
    private int lastPlatformX = 0;
    private int lastPlatformY = 0;
    
    public Opponent() {
        // Load IDLE frames (idle (1).png to idle (12).png)
        idleFrames = new GreenfootImage[12];
        for (int i = 0; i < idleFrames.length; i++) {
            String filename = "idle (" + (i + 1) + ").png";
            idleFrames[i] = new GreenfootImage(filename);
        }
        // Load RUN frames (run (1).png to run (8).png)
        runFrames = new GreenfootImage[8];
        for (int i = 0; i < runFrames.length; i++) {
            String filename = "run (" + (i + 1) + ").png";
            runFrames[i] = new GreenfootImage(filename);
        }
        // Load JUMP frames (jump (1).png to jump (4).png)
        jumpFrames = new GreenfootImage[4];
        for (int i = 0; i < jumpFrames.length; i++) {
            String filename = "jump (" + (i + 1) + ").png";
            jumpFrames[i] = new GreenfootImage(filename);
        }
        setImage(idleFrames[0]); // Start idle
    }
    
    public void act()
    {
        checkPlatformPush();
        // Movement is controlled from the world class now
        correctPlatformPenetration();
        ensureFullyOutsidePlatforms();
        applyGravity();
        // checkSpikesAndTraps(); // REMOVED - opponent immune to spikes
        // checkShurikenCollision(); // REMOVED - opponent immune to shurikens
        // checkFallOff(); // REMOVED - opponent won't be removed when falling
        animate();
    }
    
    // Public method that can be called from the world class
    public void moveOpponent(int distance)
    {
        move(distance); // Positive = right, Negative = left
        
        if (distance != 0) {
            isRunning = true;
            facingRight = (distance > 0);
        } else {
            isRunning = false;
        }
    }
    
    // Check for pixel-perfect collision with shurikens
    private void checkShurikenCollision()
    {
        // Check if opponent is still in the world
        if(getWorld() == null) return;
        
        java.util.List<Shuriken> shurikens = getIntersectingObjects(Shuriken.class);
        
        for (Shuriken shuriken : shurikens)
        {
            GreenfootImage shurikenImg = shuriken.getImage();
            GreenfootImage opponentImg = getImage();
            int dx = getX() - shuriken.getX();
            int dy = getY() - shuriken.getY();
            
            for (int x = 0; x < opponentImg.getWidth(); x++)
            {
                for (int y = 0; y < opponentImg.getHeight(); y++)
                {
                    int shurikenX = x - opponentImg.getWidth() / 2 - dx + shurikenImg.getWidth() / 2;
                    int shurikenY = y - opponentImg.getHeight() / 2 - dy + shurikenImg.getHeight() / 2;
                    
                    if (shurikenX >= 0 && shurikenX < shurikenImg.getWidth() &&
                        shurikenY >= 0 && shurikenY < shurikenImg.getHeight())
                    {
                        if (opponentImg.getColorAt(x, y).getAlpha() > 0 &&
                            shurikenImg.getColorAt(shurikenX, shurikenY).getAlpha() > 0)
                        {
                            // Check again before removing
                            if(getWorld() != null) {
                                getWorld().removeObject(this);
                            }
                            return;
                        }
                    }
                }
            }
        }
    }
    
    // Ensures opponent is fully outside all platforms
    private void ensureFullyOutsidePlatforms()
    {
        if (onGround)
        {
            return;
        }
        
        java.util.List<Platform> platforms = getIntersectingObjects(Platform.class);
        
        for (Platform platform : platforms)
        {
            if (pixelPerfectCollision(platform.getImage(), platform))
            {
                int opponentLeft = getX() - getImage().getWidth() / 2;
                int opponentRight = getX() + getImage().getWidth() / 2;
                int opponentTop = getY() - getImage().getHeight() / 2;
                int opponentBottom = getY() + getImage().getHeight() / 2;
                
                int platformLeft = platform.getX() - platform.getImage().getWidth() / 2;
                int platformRight = platform.getX() + platform.getImage().getWidth() / 2;
                int platformTop = platform.getY() - platform.getImage().getHeight() / 2;
                int platformBottom = platform.getY() + platform.getImage().getHeight() / 2;
                
                int penetrationLeft = opponentRight - platformLeft;
                int penetrationRight = platformRight - opponentLeft;
                int penetrationTop = opponentBottom - platformTop;
                int penetrationBottom = platformBottom - opponentTop;
                
                int minPenetration = Math.min(Math.min(penetrationLeft, penetrationRight),
                                             Math.min(penetrationTop, penetrationBottom));
                
                if (minPenetration <= 3)
                {
                    continue;
                }
                
                if (minPenetration == penetrationLeft && penetrationLeft > 0)
                {
                    setLocation(platformLeft - getImage().getWidth() / 2, getY());
                }
                else if (minPenetration == penetrationRight && penetrationRight > 0)
                {
                    setLocation(platformRight + getImage().getWidth() / 2, getY());
                }
                else if (minPenetration == penetrationTop && penetrationTop > 0)
                {
                    setLocation(getX(), platformTop - getImage().getHeight() / 2);
                }
                else if (minPenetration == penetrationBottom && penetrationBottom > 0)
                {
                    setLocation(getX(), platformBottom + getImage().getHeight() / 2);
                    vSpeed = 0;
                }
            }
        }
    }
    
    private void correctPlatformPenetration()
    {
        if (!isRunning || !onGround) return;
        
        java.util.List<Platform> platforms = getIntersectingObjects(Platform.class);
        
        for (Platform platform : platforms)
        {
            if (pixelPerfectCollisionForMovement(platform.getImage(), platform))
            {
                int opponentLeft = getX() - getImage().getWidth() / 2;
                int opponentRight = getX() + getImage().getWidth() / 2;
                
                int platformLeft = platform.getX() - platform.getImage().getWidth() / 2;
                int platformRight = platform.getX() + platform.getImage().getWidth() / 2;
                
                int penetrationLeft = opponentRight - platformLeft;
                int penetrationRight = platformRight - opponentLeft;
                
                if (penetrationLeft < penetrationRight && penetrationLeft > 0)
                {
                    setLocation(platformLeft - getImage().getWidth() / 2, getY());
                }
                else if (penetrationRight > 0)
                {
                    setLocation(platformRight + getImage().getWidth() / 2, getY());
                }
            }
        }
    }
    
    private void checkPlatformPush()
    {
        Platform currentPlatform = (Platform) getOneIntersectingObject(Platform.class);
        
        if (currentPlatform != null && onGround)
        {
            if (lastPlatform == currentPlatform)
            {
                int deltaX = currentPlatform.getX() - lastPlatformX;
                int deltaY = currentPlatform.getY() - lastPlatformY;
                
                if (deltaX != 0 || deltaY != 0)
                {
                    setLocation(getX() + deltaX, getY() + deltaY);
                }
            }
            
            lastPlatform = currentPlatform;
            lastPlatformX = currentPlatform.getX();
            lastPlatformY = currentPlatform.getY();
        }
        else
        {
            lastPlatform = null;
        }
    }
    
    private boolean isTouchingPlatformForMovement() {
        java.util.List<Platform> platforms = getIntersectingObjects(Platform.class);
        if (!platforms.isEmpty()) {
            for (Platform platform : platforms) {
                if (pixelPerfectCollisionForMovement(platform.getImage(), platform)) {
                    return true;
                }
            }
        }
        
        return false;
    }
    
    private boolean pixelPerfectCollisionForMovement(GreenfootImage otherImg, Actor other) {
        GreenfootImage opponentImg = getImage();
        
        int dx = getX() - other.getX();
        int dy = getY() - other.getY();
        
        int opponentW = opponentImg.getWidth();
        int opponentH = opponentImg.getHeight();
        int otherW = otherImg.getWidth();
        int otherH = otherImg.getHeight();
        
        int startY = opponentH * 2 / 3;
        
        for (int x = 0; x < opponentW; x++) {
            for (int y = startY; y < opponentH; y++) {
                int otherX = x - opponentW / 2 - dx + otherW / 2;
                int otherY = y - opponentH / 2 - dy + otherH / 2;
                
                if (otherX >= 0 && otherX < otherW && otherY >= 0 && otherY < otherH) {
                    if (opponentImg.getColorAt(x, y).getAlpha() > 0 &&
                        otherImg.getColorAt(otherX, otherY).getAlpha() > 0) {
                        return true;
                    }
                }
            }
        }
        
        return false;
    }
    
    private void applyGravity()
    {
        if (vSpeed < 9)
        {
            vSpeed = vSpeed + gravity;
        }
        
        int steps = Math.abs(vSpeed);
        int sign = vSpeed > 0 ? 1 : -1;
        
        for (int i = 0; i < steps; i++)
        {
            setLocation(getX(), getY() + sign);
            
            if (isTouchingPlatform())
            {
                setLocation(getX(), getY() - sign);
                
                if (vSpeed > 0)
                {
                    onGround = true;
                    vSpeed = 0;
                }
                else
                {
                    vSpeed = 0;
                }
                return;
            }
        }
        
        onGround = false;
    }
    
    private boolean isTouchingPlatform()
    {
        java.util.List<Platform> platforms = getIntersectingObjects(Platform.class);
        if (!platforms.isEmpty())
        {
            for (Platform platform : platforms)
            {
                if (pixelPerfectCollision(platform.getImage(), platform))
                {
                    return true;
                }
            }
        }
        
        return false;
    }
    
    private boolean pixelPerfectCollision(GreenfootImage otherImg, Actor other)
    {
        GreenfootImage opponentImg = getImage();
        
        int dx = getX() - other.getX();
        int dy = getY() - other.getY();
        
        int opponentW = opponentImg.getWidth();
        int opponentH = opponentImg.getHeight();
        int otherW = otherImg.getWidth();
        int otherH = otherImg.getHeight();
        
        for (int x = 0; x < opponentW; x++)
        {
            for (int y = 0; y < opponentH; y++)
            {
                int otherX = x - opponentW / 2 - dx + otherW / 2;
                int otherY = y - opponentH / 2 - dy + otherH / 2;
                
                if (otherX >= 0 && otherX < otherW && otherY >= 0 && otherY < otherH)
                {
                    if (opponentImg.getColorAt(x, y).getAlpha() > 0 &&
                        otherImg.getColorAt(otherX, otherY).getAlpha() > 0)
                    {
                        return true;
                    }
                }
            }
        }
        
        return false;
    }
    
    private void checkSpikesAndTraps()
    {
        java.util.List<Spike> spikes = getIntersectingObjects(Spike.class);
        
        for (Spike spike : spikes)
        {
            GreenfootImage spikeImg = spike.getImage();
            GreenfootImage opponentImg = getImage();
            int dx = getX() - spike.getX();
            int dy = getY() - spike.getY();
            
            for (int x = 0; x < opponentImg.getWidth(); x++)
            {
                for (int y = 0; y < opponentImg.getHeight(); y++)
                {
                    int spikeX = x - opponentImg.getWidth() / 2 - dx + spikeImg.getWidth() / 2;
                    int spikeY = y - opponentImg.getHeight() / 2 - dy + spikeImg.getHeight() / 2;
                    
                    if (spikeX >= 0 && spikeX < spikeImg.getWidth() &&
                        spikeY >= 0 && spikeY < spikeImg.getHeight())
                    {
                        if (opponentImg.getColorAt(x, y).getAlpha() > 0 &&
                            spikeImg.getColorAt(spikeX, spikeY).getAlpha() > 0)
                        {
                            getWorld().removeObject(this);
                            return;
                        }
                    }
                }
            }
        }
    }
    
    private void checkFallOff()
    {
        if (getY() >= 399)
        {
            getWorld().removeObject(this);
        }
    }
    
    // Public method to set movement state (call this from world)
    public void setMoving(boolean moving, boolean right)
    {
        isRunning = moving;
        facingRight = right;
    }
    
    private void animate() {
        delayCounter++;
        if (delayCounter >= animationDelay) {
            delayCounter = 0;
            frameIndex++;
            
            GreenfootImage[] currentFrames;
            
            if (!onGround) {
                currentFrames = jumpFrames;
            } else if (isRunning) {
                currentFrames = runFrames;
            } else {
                currentFrames = idleFrames;
            }
            
            if (frameIndex >= currentFrames.length) {
                frameIndex = 0;
            }
            
            GreenfootImage img = new GreenfootImage(currentFrames[frameIndex]);
            if (!facingRight) {
                img.mirrorHorizontally();
            }
            setImage(img);
        }
    }
}