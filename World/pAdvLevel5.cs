import greenfoot.*;  // (World, Actor, GreenfootImage, Greenfoot and MouseInfo)

/**
 * Write a description of class pAdvLevel5 here.
 * 
 * @author (your name) 
 * @version (a version number or a date)
 */
public class pAdvLevel5 extends World
{

    /**
     * Constructor for objects of class pAdvLevel5.
     * 
     */
     enum PlatformState {
        WAITING, MOVING, STOPPED, REMOVED, DISAPPEAR, SPAWNED, APPEAR
    }

    enum SpikeState {
        NOT_CREATED, CREATED, READY, MOVING, STOPPED, DISAPPEAR, APPEAR
    }

    
    int playerX;
    int playerY;
    Spike sp1;
    Spike sp2;
    Spike sp3;
    Spike sp4;
    Platform p6Dis;
    Platform rotating;
    Platform p9Up;
    PlatformState rotatingState = PlatformState.WAITING;
    SpikeState sp1State = SpikeState.CREATED;
    SpikeState sp2State = SpikeState.CREATED;
    SpikeState sp3State = SpikeState.CREATED;
    SpikeState sp4State = SpikeState.CREATED;
    PlatformState p9UpState = PlatformState.WAITING;
    
    // Text display variables
    private boolean textShown = false;
    private int textTimer = 0;
    private static final int TEXT_DURATION = 180; // Show for 3 seconds (60 fps * 3)
    
    // New variable to track if player was teleported
    private boolean playerTeleported = false;
    private boolean realPortalSpawned = false;
    
    public pAdvLevel5()
    {    
        // Create a new world with 600x400 cells with a cell size of 1x1 pixels.
        super(750, 400, 1);
        prepare();
    }
    
    public void  prepare(){
         
        Platform p1 = new Platform(100, 150);
        addObject(p1, 30, 340);
        rotating = new Platform(100, 20);
        addObject(rotating, 130, 275);
        Platform p2 = new Platform (50, 150);
        addObject(p2, 250, 340);
        Platform p3 = new Platform (50, 150);
        addObject(p3, 350, 340);
        Platform p4 = new Platform (50, 150);
        addObject(p4, 450, 340);
        Platform p5 = new Platform (50, 150);
        addObject(p5, 540, 340);
        p6Dis = new Platform (50, 150);
        addObject(p6Dis, 580, 340);
        Platform p7 = new Platform(100, 150);
        addObject(p7, 730, 340);
        Platform p8 = new Platform(40, 150);
        addObject(p8, 620, 340);
         p9Up = new Platform(25, 150);
        addObject(p9Up, 612, 340);
        FakeAdvPortal fp1 = new FakeAdvPortal();
        addObject(fp1, 725, 230);
        
        
        Person player1 = new Person();
        addObject(player1, 30, 246);
        pauseBtn pauseButton = new pauseBtn();
        addObject(pauseButton, 700, 30);
    }
    
    public void act(){
        updatePlayerPosition();
        displayText();
        checkFakePortalTouch();
        trapsV1();
        trapsV2();
    }
    
     public void updatePlayerPosition(){
        java.util.List<Person> players = getObjects(Person.class);
        
        if(players.isEmpty()){
            return;
        }
        
        Person player = players.get(0);
        playerX = player.getX();
        playerY = player.getY();
    }
    
    // Check if player touches fake portal and spawn real portal (using coordinates)
    public void checkFakePortalTouch(){
        if(!realPortalSpawned && playerX >= 725 && playerY >= 240){
            advPortal ap1 = new advPortal(5);
            addObject(ap1, 80, 231);
            realPortalSpawned = true;
        }
    }
    
    // Display threatening text
    public void displayText(){
        if(!textShown && playerX >= 163){
            showText("Do you remember me? Well, this is my full power", 375, 50);
            textShown = true;
            textTimer = 0;
        }
        
        if(textShown){
            textTimer++;
            if(textTimer >= TEXT_DURATION){
                showText("", 375, 50); // Clear text
            }
        }
    }
    
    boolean jump = false;
    boolean secondJump = false;
    boolean spawnOne = false;
    Shuriken s1;
    Shuriken s2;
    Shuriken s3;
    
    // Variables for unpredictable 2D shuriken movement (3 shurikens)
    private double shuriken1SpeedX = 3.0;
    private double shuriken1SpeedY = 0.0;
    private int direction1ChangeTimer = 0;
    
    private double shuriken2SpeedX = -2.5;
    private double shuriken2SpeedY = 2.0;
    private int direction2ChangeTimer = 0;
    
    private double shuriken3SpeedX = 0.0;
    private double shuriken3SpeedY = 3.0;
    private int direction3ChangeTimer = 0;
    
    private static final int DIRECTION_CHANGE_INTERVAL = 40;
    private static final double MAX_SPEED = 4.5;
    private static final double MIN_SPEED = 2.0; // Minimum speed to prevent getting stuck
    
    public void trapsV1(){
        if (rotatingState == PlatformState.WAITING && playerX >= 95 && !playerTeleported){
            rotatingState = PlatformState.MOVING;
        }
        
        // Teleport player if they go back to X=0 after opponent spawns
        if(spawnOne && playerX <= 0){
            java.util.List<Person> players = getObjects(Person.class);
            if(!players.isEmpty()){
                Person player = players.get(0);
                player.setLocation(730, 246); // Teleport to fake portal position
                playerTeleported = true;
                
                // Disable traps except shuriken and sp1
                // Remove sp2, sp3, sp4 if they exist
                if(sp2 != null && getObjects(Spike.class).contains(sp2)){
                    removeObject(sp2);
                }
                if(sp3 != null && getObjects(Spike.class).contains(sp3)){
                    removeObject(sp3);
                }
                if(sp4 != null && getObjects(Spike.class).contains(sp4)){
                    removeObject(sp4);
                }
                
                // Set spike states to disabled
                sp2State = SpikeState.DISAPPEAR;
                sp3State = SpikeState.DISAPPEAR;
                sp4State = SpikeState.DISAPPEAR;
            }
        }
        
        if(playerX >= 65 && sp1State == SpikeState.CREATED && !playerTeleported){
            sp1 = new Spike();
        addObject(sp1, 90, 257);
        sp1State = SpikeState.APPEAR;
        }
        if(sp1State == SpikeState.APPEAR && playerX >= 100 && playerY <= 213 && !jump && !playerTeleported){
            jump = true;
        }
        if(sp1State == SpikeState.APPEAR && jump && !playerTeleported){
            sp1.setLocation(sp1.getX() + 5, sp1.getY());
            
            if(sp1.getX() >= 130){
                sp1State = SpikeState.STOPPED;
            }
        }
        
        if(playerX >= 163 && sp1State == SpikeState.STOPPED && !spawnOne){
             Opponent op1 = new Opponent();
             addObject(op1, 30, 246);
             
             // Spawn 3 shurikens with different starting positions and velocities
             s1 = new Shuriken();
             addObject(s1, 50, 246);
             shuriken1SpeedX = 3.0;
             shuriken1SpeedY = 0.0;
             
             s2 = new Shuriken();
             addObject(s2, 400, 200);
             shuriken2SpeedX = -2.5;
             shuriken2SpeedY = 2.0;
             
             s3 = new Shuriken();
             addObject(s3, 350, 100);
             shuriken3SpeedX = 0.0;
             shuriken3SpeedY = 3.0;
             
             spawnOne = true;
        }
        
        // Unpredictable 2D movement for all 3 shurikens
        if(spawnOne){
            // SHURIKEN 1
            if(s1 != null){
                direction1ChangeTimer++;
                
                if(direction1ChangeTimer >= DIRECTION_CHANGE_INTERVAL){
                    if(Greenfoot.getRandomNumber(100) < 40){
                        double angle = Greenfoot.getRandomNumber(360);
                        double angleRad = Math.toRadians(angle);
                        double speed = 2.5 + (Greenfoot.getRandomNumber(30) / 10.0);
                        shuriken1SpeedX = Math.cos(angleRad) * speed;
                        shuriken1SpeedY = Math.sin(angleRad) * speed;
                    }
                    direction1ChangeTimer = 0;
                }
                
                s1.setLocation((int)(s1.getX() + shuriken1SpeedX), (int)(s1.getY() + shuriken1SpeedY));
                
                // Better edge detection with margin
                if(s1.getX() >= 735){
                    s1.setLocation(730, s1.getY());
                    shuriken1SpeedX = -Math.abs(shuriken1SpeedX);
                    shuriken1SpeedY += (Greenfoot.getRandomNumber(20) - 10) / 10.0;
                }
                if(s1.getX() <= 15){
                    s1.setLocation(20, s1.getY());
                    shuriken1SpeedX = Math.abs(shuriken1SpeedX);
                    shuriken1SpeedY += (Greenfoot.getRandomNumber(20) - 10) / 10.0;
                }
                if(s1.getY() >= 385){
                    s1.setLocation(s1.getX(), 380);
                    shuriken1SpeedY = -Math.abs(shuriken1SpeedY);
                    shuriken1SpeedX += (Greenfoot.getRandomNumber(20) - 10) / 10.0;
                }
                if(s1.getY() <= 15){
                    s1.setLocation(s1.getX(), 20);
                    shuriken1SpeedY = Math.abs(shuriken1SpeedY);
                    shuriken1SpeedX += (Greenfoot.getRandomNumber(20) - 10) / 10.0;
                }
                
                // Ensure minimum speed
                double speed1 = Math.sqrt(shuriken1SpeedX * shuriken1SpeedX + shuriken1SpeedY * shuriken1SpeedY);
                if(speed1 < MIN_SPEED){
                    shuriken1SpeedX = (shuriken1SpeedX / speed1) * MIN_SPEED;
                    shuriken1SpeedY = (shuriken1SpeedY / speed1) * MIN_SPEED;
                }
                if(speed1 > MAX_SPEED){
                    shuriken1SpeedX = (shuriken1SpeedX / speed1) * MAX_SPEED;
                    shuriken1SpeedY = (shuriken1SpeedY / speed1) * MAX_SPEED;
                }
            }
            
            // SHURIKEN 2
            if(s2 != null){
                direction2ChangeTimer++;
                
                if(direction2ChangeTimer >= DIRECTION_CHANGE_INTERVAL){
                    if(Greenfoot.getRandomNumber(100) < 40){
                        double angle = Greenfoot.getRandomNumber(360);
                        double angleRad = Math.toRadians(angle);
                        double speed = 2.5 + (Greenfoot.getRandomNumber(30) / 10.0);
                        shuriken2SpeedX = Math.cos(angleRad) * speed;
                        shuriken2SpeedY = Math.sin(angleRad) * speed;
                    }
                    direction2ChangeTimer = 0;
                }
                
                s2.setLocation((int)(s2.getX() + shuriken2SpeedX), (int)(s2.getY() + shuriken2SpeedY));
                
                if(s2.getX() >= 735){
                    s2.setLocation(730, s2.getY());
                    shuriken2SpeedX = -Math.abs(shuriken2SpeedX);
                    shuriken2SpeedY += (Greenfoot.getRandomNumber(20) - 10) / 10.0;
                }
                if(s2.getX() <= 15){
                    s2.setLocation(20, s2.getY());
                    shuriken2SpeedX = Math.abs(shuriken2SpeedX);
                    shuriken2SpeedY += (Greenfoot.getRandomNumber(20) - 10) / 10.0;
                }
                if(s2.getY() >= 385){
                    s2.setLocation(s2.getX(), 380);
                    shuriken2SpeedY = -Math.abs(shuriken2SpeedY);
                    shuriken2SpeedX += (Greenfoot.getRandomNumber(20) - 10) / 10.0;
                }
                if(s2.getY() <= 15){
                    s2.setLocation(s2.getX(), 20);
                    shuriken2SpeedY = Math.abs(shuriken2SpeedY);
                    shuriken2SpeedX += (Greenfoot.getRandomNumber(20) - 10) / 10.0;
                }
                
                double speed2 = Math.sqrt(shuriken2SpeedX * shuriken2SpeedX + shuriken2SpeedY * shuriken2SpeedY);
                if(speed2 < MIN_SPEED){
                    shuriken2SpeedX = (shuriken2SpeedX / speed2) * MIN_SPEED;
                    shuriken2SpeedY = (shuriken2SpeedY / speed2) * MIN_SPEED;
                }
                if(speed2 > MAX_SPEED){
                    shuriken2SpeedX = (shuriken2SpeedX / speed2) * MAX_SPEED;
                    shuriken2SpeedY = (shuriken2SpeedY / speed2) * MAX_SPEED;
                }
            }
            
            // SHURIKEN 3
            if(s3 != null){
                direction3ChangeTimer++;
                
                if(direction3ChangeTimer >= DIRECTION_CHANGE_INTERVAL){
                    if(Greenfoot.getRandomNumber(100) < 40){
                        double angle = Greenfoot.getRandomNumber(360);
                        double angleRad = Math.toRadians(angle);
                        double speed = 2.5 + (Greenfoot.getRandomNumber(30) / 10.0);
                        shuriken3SpeedX = Math.cos(angleRad) * speed;
                        shuriken3SpeedY = Math.sin(angleRad) * speed;
                    }
                    direction3ChangeTimer = 0;
                }
                
                s3.setLocation((int)(s3.getX() + shuriken3SpeedX), (int)(s3.getY() + shuriken3SpeedY));
                
                if(s3.getX() >= 735){
                    s3.setLocation(730, s3.getY());
                    shuriken3SpeedX = -Math.abs(shuriken3SpeedX);
                    shuriken3SpeedY += (Greenfoot.getRandomNumber(20) - 10) / 10.0;
                }
                if(s3.getX() <= 15){
                    s3.setLocation(20, s3.getY());
                    shuriken3SpeedX = Math.abs(shuriken3SpeedX);
                    shuriken3SpeedY += (Greenfoot.getRandomNumber(20) - 10) / 10.0;
                }
                if(s3.getY() >= 385){
                    s3.setLocation(s3.getX(), 380);
                    shuriken3SpeedY = -Math.abs(shuriken3SpeedY);
                    shuriken3SpeedX += (Greenfoot.getRandomNumber(20) - 10) / 10.0;
                }
                if(s3.getY() <= 15){
                    s3.setLocation(s3.getX(), 20);
                    shuriken3SpeedY = Math.abs(shuriken3SpeedY);
                    shuriken3SpeedX += (Greenfoot.getRandomNumber(20) - 10) / 10.0;
                }
                
                double speed3 = Math.sqrt(shuriken3SpeedX * shuriken3SpeedX + shuriken3SpeedY * shuriken3SpeedY);
                if(speed3 < MIN_SPEED){
                    shuriken3SpeedX = (shuriken3SpeedX / speed3) * MIN_SPEED;
                    shuriken3SpeedY = (shuriken3SpeedY / speed3) * MIN_SPEED;
                }
                if(speed3 > MAX_SPEED){
                    shuriken3SpeedX = (shuriken3SpeedX / speed3) * MAX_SPEED;
                    shuriken3SpeedY = (shuriken3SpeedY / speed3) * MAX_SPEED;
                }
            }
        }
        
        // Disable sp2 trap if player was teleported
        if(!playerTeleported){
            if(playerX >= 205 && playerY <= 210 && sp2State == SpikeState.CREATED){
                sp2 = new Spike();
            addObject(sp2, 250, 257);
            sp2State = SpikeState.APPEAR;
            }
            
            if(sp2State == SpikeState.APPEAR && playerX <= 180){
                removeObject(sp2);
                sp2State = SpikeState.DISAPPEAR;
            }
            
             if(playerX >= 300 && playerY <= 200 && sp3State == SpikeState.CREATED
             && sp2State == SpikeState.DISAPPEAR){
                sp3 = new Spike();
            addObject(sp3, 350, 257);
            sp3State = SpikeState.APPEAR;
            }
            
            if(sp3State == SpikeState.APPEAR && playerX <= 270){
                removeObject(sp3);
                sp3State = SpikeState.DISAPPEAR;
            }
        }
       
    }
    
    boolean goDown = false;
    boolean oneTime = false;
    public void trapsV2(){
        // Disable sp4 and platform traps if player was teleported
        if(!playerTeleported){
            if(playerX >= 395 && playerY <= 200 && sp4State == SpikeState.CREATED
             && sp3State == SpikeState.DISAPPEAR){
                sp4 = new Spike();
            addObject(sp4, 450, 257);
            sp4State = SpikeState.APPEAR;
            }
            
            if(sp4State == SpikeState.APPEAR && playerX <= 370){
                removeObject(sp4);
                sp4State = SpikeState.DISAPPEAR;
            }
            
            if(sp4State == SpikeState.DISAPPEAR && playerX >= 495 && playerY <= 195
            && !secondJump){
                secondJump = true;
            }
            
            if(secondJump && playerX <= 470 && !oneTime){
                sp4 = new Spike();
            addObject(sp4, 450, 257);
            }
            
            if(playerX >= 555){
                removeObject(p6Dis);
            }
            
            if(p9UpState == PlatformState.WAITING && playerX >= 572 
            && playerY <= 184){
                p9UpState = PlatformState.MOVING;
            }
            
            if(p9UpState == PlatformState.MOVING){
                p9Up.setLocation(p9Up.getX(), p9Up.getY() - 20);
                
                if(p9Up.getY() <= 210){
                    p9UpState = PlatformState.STOPPED;
                }
            }
            
            if(!goDown && p9UpState == PlatformState.STOPPED && playerX <= 560){
                goDown = true;
            }
            
            if (goDown && p9UpState == PlatformState.STOPPED){
                p9Up.setLocation(p9Up.getX(), p9Up.getY() + 20);
                 if(p9Up.getY()>= 390){
                    p9UpState = PlatformState.DISAPPEAR;
                }
            }
        }
        
       //195
       //450, 127
    }
    
    
}