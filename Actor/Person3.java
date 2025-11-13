import greenfoot.*;

public class Person3 extends Actor
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
    
    // Platform tracking
    private Platform lastPlatform = null;
    private int lastPlatformX = 0;
    private int lastPlatformY = 0;

    public Person3() {
        // LOAD IDLE (boy1.png → boy10.png)
        idleFrames = new GreenfootImage[10];
        for (int i = 0; i < idleFrames.length; i++) {
            idleFrames[i] = new GreenfootImage("boy" + (i + 1) + ".png");
        }

        // LOAD RUN (boyRun0.png → boyRun8.png)
        runFrames = new GreenfootImage[9];
        for (int i = 0; i < runFrames.length; i++) {
            runFrames[i] = new GreenfootImage("boyRun" + i + ".png");
        }

        // LOAD JUMP (boyJump1.png → boyJump4.png)
        jumpFrames = new GreenfootImage[4];
        for (int i = 0; i < jumpFrames.length; i++) {
            jumpFrames[i] = new GreenfootImage("boyJump" + (i + 1) + ".png");
        }

        setImage(idleFrames[0]);
    }

    public void act() {
        checkPlatformPush();
        handleMovement();
        correctPlatformPenetration();
        ensureFullyOutsidePlatforms();
        applyGravity();
        checkSpikesAndTraps();
        checkFallOff();
        nextLevel();
        animate();
    }

    private void ensureFullyOutsidePlatforms() {
        if (onGround) return;
        java.util.List<Platform> platforms = getIntersectingObjects(Platform.class);

        for (Platform platform : platforms) {
            if (pixelPerfectCollision(platform.getImage(), platform)) {

                int personLeft = getX() - getImage().getWidth() / 2;
                int personRight = getX() + getImage().getWidth() / 2;
                int personTop = getY() - getImage().getHeight() / 2;
                int personBottom = getY() + getImage().getHeight() / 2;

                int platformLeft = platform.getX() - platform.getImage().getWidth() / 2;
                int platformRight = platform.getX() + platform.getImage().getWidth() / 2;
                int platformTop = platform.getY() - platform.getImage().getHeight() / 2;
                int platformBottom = platform.getY() + platform.getImage().getHeight() / 2;

                int penetrationLeft = personRight - platformLeft;
                int penetrationRight = platformRight - personLeft;
                int penetrationTop = personBottom - platformTop;
                int penetrationBottom = platformBottom - personTop;

                int minPenetration = Math.min(Math.min(penetrationLeft, penetrationRight),
                                              Math.min(penetrationTop, penetrationBottom));

                if (minPenetration <= 3) continue;

                if (minPenetration == penetrationLeft && penetrationLeft > 0)
                    setLocation(platformLeft - getImage().getWidth() / 2, getY());
                else if (minPenetration == penetrationRight && penetrationRight > 0)
                    setLocation(platformRight + getImage().getWidth() / 2, getY());
                else if (minPenetration == penetrationTop && penetrationTop > 0)
                    setLocation(getX(), platformTop - getImage().getHeight() / 2);
                else if (minPenetration == penetrationBottom && penetrationBottom > 0) {
                    setLocation(getX(), platformBottom + getImage().getHeight() / 2);
                    vSpeed = 0;
                }
            }
        }
    }

    private void correctPlatformPenetration() {
        if (!isRunning || !onGround) return;
        java.util.List<Platform> platforms = getIntersectingObjects(Platform.class);

        for (Platform platform : platforms) {
            if (pixelPerfectCollisionForMovement(platform.getImage(), platform)) {

                int personLeft = getX() - getImage().getWidth() / 2;
                int personRight = getX() + getImage().getWidth() / 2;
                int platformLeft = platform.getX() - platform.getImage().getWidth() / 2;
                int platformRight = platform.getX() + platform.getImage().getWidth() / 2;

                int penetrationLeft = personRight - platformLeft;
                int penetrationRight = platformRight - personLeft;

                if (penetrationLeft < penetrationRight && penetrationLeft > 0)
                    setLocation(platformLeft - getImage().getWidth() / 2, getY());
                else if (penetrationRight > 0)
                    setLocation(platformRight + getImage().getWidth() / 2, getY());
            }
        }
    }

    private void checkPlatformPush() {
        Platform currentPlatform = (Platform)getOneIntersectingObject(Platform.class);

        if (currentPlatform != null && onGround) {
            if (lastPlatform == currentPlatform) {
                int dx = currentPlatform.getX() - lastPlatformX;
                int dy = currentPlatform.getY() - lastPlatformY;
                if (dx != 0 || dy != 0)
                    setLocation(getX() + dx, getY() + dy);
            }
            lastPlatform = currentPlatform;
            lastPlatformX = currentPlatform.getX();
            lastPlatformY = currentPlatform.getY();
        } else {
            lastPlatform = null;
        }
    }

    private void handleMovement() {
        isRunning = false;
        int speed = onGround ? moveSpeed : airSpeed;

        if (Greenfoot.isKeyDown("a")) {
            facingRight = false;
            isRunning = true;
            setLocation(getX() - speed, getY());
        }
        else if (Greenfoot.isKeyDown("d")) {
            facingRight = true;
            isRunning = true;
            setLocation(getX() + speed, getY());
        }

        if ((Greenfoot.isKeyDown("space") || Greenfoot.isKeyDown("w")) && onGround)
            jump();
    }

    private boolean pixelPerfectCollisionForMovement(GreenfootImage otherImg, Actor other) {
        GreenfootImage img = getImage();
        int dx = getX() - other.getX();
        int dy = getY() - other.getY();
        int w = img.getWidth();
        int h = img.getHeight();
        int ow = otherImg.getWidth();
        int oh = otherImg.getHeight();
        int startY = h * 2 / 3;

        for (int x = 0; x < w; x++)
            for (int y = startY; y < h; y++) {
                int ox = x - w / 2 - dx + ow / 2;
                int oy = y - h / 2 - dy + oh / 2;
                if (ox >= 0 && ox < ow && oy >= 0 && oy < oh)
                    if (img.getColorAt(x,y).getAlpha() > 0 &&
                        otherImg.getColorAt(ox,oy).getAlpha() > 0)
                        return true;
            }
        return false;
    }

    private void jump() {
        vSpeed = jumpStrength;
        onGround = false;
    }

    private void applyGravity() {
        if (vSpeed < 9) vSpeed += gravity;
        int steps = Math.abs(vSpeed);
        int sign = vSpeed > 0 ? 1 : -1;

        for (int i = 0; i < steps; i++) {
            setLocation(getX(), getY() + sign);

            if (isTouchingPlatform()) {
                setLocation(getX(), getY() - sign);
                if (vSpeed > 0) onGround = true;
                vSpeed = 0;
                return;
            }
        }
        onGround = false;
    }

    private boolean isTouchingPlatform() {
        java.util.List<Platform> platforms = getIntersectingObjects(Platform.class);
        for (Platform p : platforms)
            if (pixelPerfectCollision(p.getImage(), p))
                return true;
        return false;
    }

    private boolean pixelPerfectCollision(GreenfootImage otherImg, Actor other) {
        GreenfootImage img = getImage();
        int dx = getX() - other.getX();
        int dy = getY() - other.getY();
        int w = img.getWidth();
        int h = img.getHeight();
        int ow = otherImg.getWidth();
        int oh = otherImg.getHeight();

        for (int x = 0; x < w; x++)
            for (int y = 0; y < h; y++) {
                int ox = x - w / 2 - dx + ow / 2;
                int oy = y - h / 2 - dy + oh / 2;

                if (ox >= 0 && ox < ow && oy >= 0 && oy < oh)
                    if (img.getColorAt(x,y).getAlpha() > 0 &&
                        otherImg.getColorAt(ox,oy).getAlpha() > 0)
                        return true;
            }
        return false;
    }

    private void checkSpikesAndTraps() {
        java.util.List<Spike> spikes = getIntersectingObjects(Spike.class);

        for (Spike spike : spikes)
            if (pixelPerfectCollision(spike.getImage(), spike)) {
                Greenfoot.setWorld(new easyLevel1());
                return;
            }
    }

    private void checkFallOff() {
        if (getY() >= 399)
            Greenfoot.setWorld(new easyLevel1());
    }

    public void nextLevel() {
        World w = getWorld();

        if (getOneIntersectingObject(easyPortal.class)!=null) {
            if (w instanceof easyLevel1) { LevelProgress.completeEasyLevel(1); Greenfoot.setWorld(new easyLevel2()); }
            else if (w instanceof easyLevel2) { LevelProgress.completeEasyLevel(2); Greenfoot.setWorld(new easyLevel3()); }
            else if (w instanceof easyLevel3) { LevelProgress.completeEasyLevel(3); Greenfoot.setWorld(new easyLevel4()); }
            else if (w instanceof easyLevel4) { LevelProgress.completeEasyLevel(4); Greenfoot.setWorld(new easyLevel5()); }
            else if (w instanceof easyLevel5) { LevelProgress.completeEasyLevel(5); Greenfoot.setWorld(new categoryEasy()); }
        }

        else if (getOneIntersectingObject(mediumPortal.class)!=null) {
            if (w instanceof mediumLevel1) { LevelProgress.completeMediumLevel(1); Greenfoot.setWorld(new mediumLevel2()); }
            else if (w instanceof mediumLevel2) { LevelProgress.completeMediumLevel(2); Greenfoot.setWorld(new mediumLevel3()); }
            else if (w instanceof mediumLevel3) { LevelProgress.completeMediumLevel(3); Greenfoot.setWorld(new mediumLevel4()); }
            else if (w instanceof mediumLevel4) { LevelProgress.completeMediumLevel(4); Greenfoot.setWorld(new mediumLevel5()); }
            else if (w instanceof mediumLevel5) { LevelProgress.completeMediumLevel(5); Greenfoot.setWorld(new categoryMedium()); }
        }

        else if (getOneIntersectingObject(hardPortal.class)!=null) {
            if (w instanceof ohardLevel1) { LevelProgress.completeHardLevel(1); Greenfoot.setWorld(new ohardLevel2()); }
            else if (w instanceof ohardLevel2) { LevelProgress.completeHardLevel(2); Greenfoot.setWorld(new ohardLevel3()); }
            else if (w instanceof ohardLevel3) { LevelProgress.completeHardLevel(3); Greenfoot.setWorld(new ohardLevel4()); }
            else if (w instanceof ohardLevel4) { LevelProgress.completeHardLevel(4); Greenfoot.setWorld(new ohardLevel5()); }
            else if (w instanceof ohardLevel5) { LevelProgress.completeHardLevel(5); Greenfoot.setWorld(new categoryHard()); }
        }

        else if (getOneIntersectingObject(advPortal.class)!=null) {
            if (w instanceof pAdvLevel1) { LevelProgress.completeAdvLevel(1); Greenfoot.setWorld(new pAdvLevel2()); }
            else if (w instanceof pAdvLevel2) { LevelProgress.completeAdvLevel(2); Greenfoot.setWorld(new pAdvLevel3()); }
            else if (w instanceof pAdvLevel3) { LevelProgress.completeAdvLevel(3); Greenfoot.setWorld(new pAdvLevel4()); }
            else if (w instanceof pAdvLevel4) { LevelProgress.completeAdvLevel(4); Greenfoot.setWorld(new pAdvLevel5()); }
            else if (w instanceof pAdvLevel5) { LevelProgress.completeAdvLevel(5); Greenfoot.setWorld(new categoryAdvance()); }
        }
    }

    private void animate() {
        delayCounter++;
        if (delayCounter >= animationDelay) {
            delayCounter = 0;
            frameIndex++;

            GreenfootImage[] frames;
            if (!onGround)
                frames = jumpFrames;
            else if (isRunning)
                frames = runFrames;
            else
                frames = idleFrames;

            if (frameIndex >= frames.length)
                frameIndex = 0;

            GreenfootImage img = new GreenfootImage(frames[frameIndex]);
            if (!facingRight) img.mirrorHorizontally();
            setImage(img);
        }
    }
}
