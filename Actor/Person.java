import greenfoot.*;

public class Person extends Actor
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

    // Moving platform tracking
    private Platform lastPlatform = null;
    private int lastPlatformX = 0;
    private int lastPlatformY = 0;

    public Person() {
        idleFrames = loadFrames("idle", 12);
        runFrames = loadFrames("run", 8);
        jumpFrames = loadFrames("jump", 4);
        setImage(idleFrames[0]);
    }

    private GreenfootImage[] loadFrames(String name, int count) {
        GreenfootImage[] frames = new GreenfootImage[count];
        for(int i=0;i<count;i++){
            frames[i] = new GreenfootImage(name+" ("+(i+1)+").png");
        }
        return frames;
    }

    public void act()
    {
        checkPlatformPush();
        handleMovement();
        correctPlatformPenetration();
        ensureFullyOutsidePlatforms();
        applyGravity();
        checkSpikesAndTraps();
        checkShurikenCollision();
        checkFallOff();
        // REMOVED: nextLevel() - now handled by easyPortal
        animate();
    }

    private void checkShurikenCollision(){
        java.util.List<Shuriken> shurikens = getIntersectingObjects(Shuriken.class);

        for(Shuriken s : shurikens){
            if(pixelPerfectCollision(s.getImage(), s)){
                resetToCurrentLevel();
                return;
            }
        }
    }

    // --- PLATFORM CORRECTION & MOVEMENT METHODS ---
    private void ensureFullyOutsidePlatforms(){
        if(onGround) return;
        java.util.List<Platform> platforms = getIntersectingObjects(Platform.class);
        for(Platform platform : platforms){
            if(pixelPerfectCollision(platform.getImage(), platform)){
                adjustOutOfPlatform(platform);
            }
        }
    }

    private void adjustOutOfPlatform(Platform platform){
        int pL = getX() - getImage().getWidth()/2;
        int pR = getX() + getImage().getWidth()/2;
        int pT = getY() - getImage().getHeight()/2;
        int pB = getY() + getImage().getHeight()/2;

        int oL = platform.getX() - platform.getImage().getWidth()/2;
        int oR = platform.getX() + platform.getImage().getWidth()/2;
        int oT = platform.getY() - platform.getImage().getHeight()/2;
        int oB = platform.getY() + platform.getImage().getHeight()/2;

        int pushLeft = pR - oL;
        int pushRight = oR - pL;
        int pushUp = pB - oT;
        int pushDown = oB - pT;

        int minPen = Math.min(Math.min(pushLeft, pushRight), Math.min(pushUp, pushDown));

        if(minPen == pushLeft) setLocation(oL - getImage().getWidth()/2, getY());
        else if(minPen == pushRight) setLocation(oR + getImage().getWidth()/2, getY());
        else if(minPen == pushUp) setLocation(getX(), oT - getImage().getHeight()/2);
        else { setLocation(getX(), oB + getImage().getHeight()/2); vSpeed = 0; }
    }

    private void correctPlatformPenetration() {
        if(!isRunning || !onGround) return;
        java.util.List<Platform> platforms = getIntersectingObjects(Platform.class);
        for(Platform p : platforms){
            if(pixelPerfectCollisionForMovement(p.getImage(), p)){
                adjustOutOfPlatform(p);
            }
        }
    }

    private void checkPlatformPush(){
        Platform platform = (Platform)getOneIntersectingObject(Platform.class);
        if(platform != null && onGround){
            if(platform == lastPlatform){
                setLocation(getX() + (platform.getX()-lastPlatformX),
                            getY() + (platform.getY()-lastPlatformY));
            }
            lastPlatform = platform;
            lastPlatformX = platform.getX();
            lastPlatformY = platform.getY();
        }else lastPlatform = null;
    }

    private void handleMovement()
    {
        isRunning = false;
        int speed = onGround ? moveSpeed : airSpeed;

        boolean reverse = (getWorld() instanceof mediumLevel1 || 
                           getWorld() instanceof mediumLevel5 ||
                           getWorld() instanceof ohardLevel3);

        if(reverse){
            if(Greenfoot.isKeyDown("a")) moveRight(speed);
            else if(Greenfoot.isKeyDown("d")) moveLeft(speed);
        } else {
            if(Greenfoot.isKeyDown("a")) moveLeft(speed);
            else if(Greenfoot.isKeyDown("d")) moveRight(speed);
        }

        if((Greenfoot.isKeyDown("space") || Greenfoot.isKeyDown("w")) && onGround){
            jump();
        }
    }

    private void moveLeft(int s){ facingRight=false; isRunning=true; setLocation(getX()-s,getY()); }
    private void moveRight(int s){ facingRight=true; isRunning=true; setLocation(getX()+s,getY()); }

    private void jump(){ vSpeed = jumpStrength; onGround = false; }

    private void applyGravity(){
        if(vSpeed < 9) vSpeed += gravity;

        int step = Math.abs(vSpeed);
        int sign = vSpeed>0? 1 : -1;

        for(int i=0;i<step;i++){
            setLocation(getX(), getY()+sign);
            if(isTouchingPlatform()){
                setLocation(getX(), getY()-sign);
                if(vSpeed>0){ onGround=true; vSpeed=0; }
                else vSpeed=0;
                return;
            }
        }
        onGround=false;
    }

    private boolean isTouchingPlatform(){
        java.util.List<Platform> platforms = getIntersectingObjects(Platform.class);
        for(Platform p : platforms){
            if(pixelPerfectCollision(p.getImage(), p)) return true;
        }
        return false;
    }

    private boolean pixelPerfectCollision(GreenfootImage o, Actor a){
        return checkAlphaOverlap(getImage(), getX(), getY(), o, a.getX(), a.getY());
    }

    private boolean pixelPerfectCollisionForMovement(GreenfootImage o, Actor a){
        GreenfootImage me = getImage();
        int startY = me.getHeight() * 2 / 3;
        return checkAlphaOverlap(me, getX(), getY(), o, a.getX(), a.getY(), startY);
    }

    private boolean checkAlphaOverlap(GreenfootImage pImg, int pX, int pY,
                                      GreenfootImage oImg, int oX, int oY){
        return checkAlphaOverlap(pImg,pX,pY,oImg,oX,oY,0);
    }

    private boolean checkAlphaOverlap(GreenfootImage pImg, int pX, int pY,
                                      GreenfootImage oImg, int oX, int oY,
                                      int startY){
        int dx = pX - oX;
        int dy = pY - oY;
        for(int x=0;x<pImg.getWidth();x++){
            for(int y=startY;y<pImg.getHeight();y++){
                int ox = x - pImg.getWidth()/2 - dx + oImg.getWidth()/2;
                int oy = y - pImg.getHeight()/2 - dy + oImg.getHeight()/2;
                if(ox>=0 && oy>=0 && ox<oImg.getWidth() && oy<oImg.getHeight()){
                    if(pImg.getColorAt(x,y).getAlpha()>0 &&
                       oImg.getColorAt(ox,oy).getAlpha()>0)
                        return true;
                }
            }
        }
        return false;
    }

    private void checkSpikesAndTraps(){
        java.util.List<Spike> spikes = getIntersectingObjects(Spike.class);
        for(Spike s : spikes){
            if(pixelPerfectCollision(s.getImage(),s)){
                resetToCurrentLevel();
                return;
            }
        }
    }

    private void checkFallOff(){
        if(getY()>=399){ 
            resetToCurrentLevel();
        }
    }

    // Helper method to reset to current level
    private void resetToCurrentLevel() {
        World w = getWorld();
        if(w instanceof easyLevel1){ Greenfoot.setWorld(new easyLevel1()); }
        else if(w instanceof easyLevel2){ Greenfoot.setWorld(new easyLevel2()); }
        else if(w instanceof easyLevel3){ Greenfoot.setWorld(new easyLevel3()); }
        else if(w instanceof easyLevel4){ Greenfoot.setWorld(new easyLevel4()); }
        else if(w instanceof easyLevel5){ Greenfoot.setWorld(new easyLevel5()); }
        // Add other level types as needed
    }

    private void animate(){
        delayCounter++;
        if(delayCounter>=animationDelay){
            delayCounter=0;
            frameIndex++;
            GreenfootImage[] f = (!onGround)? jumpFrames : (isRunning? runFrames: idleFrames);
            if(frameIndex>=f.length) frameIndex=0;
            GreenfootImage img = new GreenfootImage(f[frameIndex]);
            if(!facingRight) img.mirrorHorizontally();
            setImage(img);
        }
    }
}